using QuesterAssistant.Classes.Common;
using QuesterAssistant.Classes.Extensions;
using QuesterAssistant.Classes.PushBulletClient.Filters;
using QuesterAssistant.Classes.PushBulletClient.Models.Requests;
using QuesterAssistant.Classes.PushBulletClient.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Xml.Serialization;

namespace QuesterAssistant.Classes.PushBulletClient
{
    [Serializable]
    public class PushBulletClientLite :  NotifyHashChanged, IParse<PushBulletClientLite>
    {
        #region Constructors

        public PushBulletClientLite() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="PushBulletManager"/> class.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <exception cref="System.ArgumentNullException">accessToken</exception>
        public PushBulletClientLite(string accessToken, TimeZoneInfo timeZoneInfo = null)
        {
            if(string.IsNullOrWhiteSpace(accessToken))
            {
                throw new ArgumentNullException("accessToken");
            }

            if(timeZoneInfo != null)
            {
                TimeZoneInfo = timeZoneInfo;
            }

            AccessToken = accessToken;
        }
        /// <summary>
        /// Gets the access token.
        /// </summary>
        /// <value>
        /// The access token.
        /// </value>
        public string AccessToken { get; set; } = string.Empty;
        /// <summary>
        /// Gets the time zone information.
        /// </summary>
        /// <value>
        /// The time zone information.
        /// </value>
        [XmlIgnore]
        public TimeZoneInfo TimeZoneInfo { get; } = TimeZoneInfo.Utc;

        #endregion properties

        #region public methods

        #region User Information Methods

        /// <summary>
        /// Currents the users information.
        /// </summary>
        /// <returns></returns>
        public User CurrentUsersInformation()
        {
            try
            {
                #region processing

                User result = GetRequest<User>(string.Concat(Constants.BaseUrl, Constants.UsersUrls.Me));
                return result;

                #endregion processing
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Currents the users devices.
        /// </summary>
        /// <param name="showActiveOnly">if set to <c>true</c> [show active only].</param>
        /// <returns></returns>
        public UserDevices CurrentUsersDevices(bool showActiveOnly = false)
        {
            try
            {
                #region pre-processing

                string additionalQuery = string.Empty;

                if (showActiveOnly)
                {
                    additionalQuery = "?active=true";
                }

                #endregion end pre-processing

                #region processing

                UserDevices result = GetRequest<UserDevices>(string.Concat(Constants.BaseUrl, Constants.DevicesUrls.Me, additionalQuery).Trim());
                return result;

                #endregion processing
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion User Information Methods

        #region Push Methods

        /// <summary>
        /// Pushes the note.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="ignoreEmptyFields">if set to <c>true</c> [ignore empty fields].</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">note request</exception>
        /// <exception cref="System.Exception">
        /// </exception>
        public PushResponse PushNote(PushNoteRequest request, bool ignoreEmptyFields = false)
        {
            try
            {
                #region pre-processing

                if (request == null)
                {
                    throw new ArgumentNullException("note request");
                }

                if (string.IsNullOrWhiteSpace(request.Type))
                {
                    throw new Exception(Constants.PushRequestErrorMessages.EmptyTypeProperty);
                }

                if (!ignoreEmptyFields)
                {
                    if (string.IsNullOrWhiteSpace(request.Title))
                    {
                        throw new Exception(Constants.PushNoteRequestErrorMessages.EmptyTitleProperty);
                    }

                    if (string.IsNullOrWhiteSpace(request.Body))
                    {
                        throw new Exception(Constants.PushNoteRequestErrorMessages.EmptyBodyProperty);
                    }
                }

                #endregion pre-processing


                #region processing

                return PostPushRequest<PushNoteRequest>(request);

                #endregion processing
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets the pushes.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">
        /// filter
        /// or
        /// filter
        /// </exception>
        /// <exception cref="System.Exception">Connect issue.</exception>
        public PushResponseContainer GetPushes(PushResponseFilter filter)
        {
            try
            {
                #region pre-processing

                if (filter == null)
                {
                    throw new ArgumentNullException("filter");
                }

                string queryString = string.Empty;
                List<string> queryStringList = new List<string>();

                if (!string.IsNullOrWhiteSpace(filter.Cursor))
                {
                    string cursorQueryString = string.Format("cursor={0}", filter.Cursor);
                    queryStringList.Add(cursorQueryString);
                }
                else
                {
                    if (filter.ModifiedDate != null)
                    {
                        string modifiedDate = filter.ModifiedDate.DateTimeToUnixTime().ToString(System.Globalization.CultureInfo.InvariantCulture);
                        string modifiedDateQueryString = string.Format("modified_after={0}", modifiedDate);
                        queryStringList.Add(modifiedDateQueryString);
                    }

                    if (filter.Active != null)
                    {
                        string activeQueryString = string.Format("active={0}", ((bool)filter.Active).ToString().ToLower());
                        queryStringList.Add(activeQueryString);
                    }

                    if(filter.Limit > 0)
                    {
                        string limitQueryString = string.Format("limit={0}", filter.Limit);
                        queryStringList.Add(limitQueryString);
                    }
                }

                //Email filtering can be done on either cursor or regular queries
                if (!string.IsNullOrWhiteSpace(filter.Email))
                {
                    string emailQueryString = string.Format("email={0}", filter.Email);
                    queryStringList.Add(emailQueryString);
                }

                //Join all of the query strings
                if (queryStringList.Count() > 0)
                {
                    queryString = string.Concat("?", string.Join("&", queryStringList));
                }

                #endregion


                #region processing

                PushResponseContainer results = new PushResponseContainer();
                BasicPushResponseContainer basicPushContainer = GetRequest<BasicPushResponseContainer>(string.Concat(Constants.BaseUrl, Constants.PushesUrls.Pushes, queryString).Trim());
                PushResponseContainer pushContainer = ConvertBasicPushResponseContainer(basicPushContainer);

                if (filter.IncludeTypes != null && filter.IncludeTypes.Count() > 0)
                {
                    foreach (var type in filter.IncludeTypes)
                    {
                        results.Pushes.AddRange(pushContainer.Pushes.Where(o => o.Type == type).ToList());
                    }
                    results.Pushes = results.Pushes.OrderByDescending(o => o.Created).ToList();
                }
                else
                {
                    results = pushContainer;
                }

                return results;

                #endregion processing
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Converts the basic push response container.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <returns></returns>
        private PushResponseContainer ConvertBasicPushResponseContainer(BasicPushResponseContainer container)
        {
            PushResponseContainer result = new PushResponseContainer();
            foreach(var basicPush in container.Pushes)
            {
                result.Pushes.Add(ConvertBasicPushResponse(basicPush));
            }
            result.Cursor = container.Cursor;
            return result;
        }

        #endregion Push Methods

        #endregion public methods

        #region private methods

        /// <summary>
        /// Gets the request.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        private T GetRequest<T>(string url)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add(Constants.HeadersConstants.AuthorizationKey, string.Format(Constants.HeadersConstants.AuthorizationValue, this.AccessToken));
            HttpClient client = new HttpClient();
            var response = client.SendAsync(request).Result;

            switch((int)response.StatusCode)
            {
                case (int)HttpStatusCode.OK:
                    {
                        var result = response.Content.ReadAsStringAsync().Result;
                        var output = result.JsonToOjbect<T>();
                        return output;
                    }
                default:
                    HandleOtherStatusCodes(response.StatusCode);
                    throw new HttpRequestException(string.Format(Constants.StatusCodeExceptions.Default, (int)response.StatusCode, response.StatusCode));
            }
        }


        /// <summary>
        /// Posts the request.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url">The URL.</param>
        /// <param name="requestObject">The request object.</param>
        /// <returns></returns>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        private T PostRequest<T>(string url, object requestObject)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Headers.Add(Constants.HeadersConstants.AuthorizationKey, string.Format(Constants.HeadersConstants.AuthorizationValue, this.AccessToken));
            request.Content = new StringContent(requestObject.ToJson(), Encoding.UTF8, Constants.MimeTypes.Json);
            
            HttpClient client = new HttpClient();
            var response = client.SendAsync(request).Result;

            switch ((int)response.StatusCode)
            {
                case (int)HttpStatusCode.OK:
                    {
                        var result = response.Content.ReadAsStringAsync().Result;
                        var output = result.JsonToOjbect<T>();
                        return output;
                    }
                default:
                    HandleOtherStatusCodes(response.StatusCode);
                    throw new HttpRequestException(string.Format(Constants.StatusCodeExceptions.Default, (int)response.StatusCode, response.StatusCode));
            }
        }

        /// <summary>
        /// Handles the other status codes.
        /// </summary>
        /// <param name="statusCode">The status code.</param>
        /// <exception cref="System.Net.Http.HttpRequestException">
        /// </exception>
        private void HandleOtherStatusCodes(HttpStatusCode statusCode)
        {
            switch((int)statusCode)
            {
                case (int)HttpStatusCode.BadRequest:
                    throw new HttpRequestException(Constants.StatusCodeExceptions.BadRequest);
                case (int)HttpStatusCode.Unauthorized:
                    throw new HttpRequestException(Constants.StatusCodeExceptions.Unauthorized);
                case (int)HttpStatusCode.Forbidden:
                    throw new HttpRequestException(Constants.StatusCodeExceptions.Forbidden);
                case (int)HttpStatusCode.NotFound:
                    throw new HttpRequestException(Constants.StatusCodeExceptions.NotFound);
                case 429:
                    throw new HttpRequestException(Constants.StatusCodeExceptions.BadRequest);
                case (int)HttpStatusCode.InternalServerError:
                case (int)HttpStatusCode.NotImplemented:
                case (int)HttpStatusCode.BadGateway:
                case (int)HttpStatusCode.ServiceUnavailable:
                case (int)HttpStatusCode.GatewayTimeout:
                case (int)HttpStatusCode.HttpVersionNotSupported:
                    throw new HttpRequestException(string.Format(Constants.StatusCodeExceptions.FiveHundredXX, (int)statusCode, statusCode));
            }
        }

        /// <summary>
        /// Posts the push request.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="requestObject">The request object.</param>
        /// <returns></returns>
        private PushResponse PostPushRequest<T>(T requestObject)
        {
            var basicResponse = PostRequest<BasicPushResponse>(string.Concat(Constants.BaseUrl, Constants.PushesUrls.Pushes), requestObject);
            PushResponse response = ConvertBasicPushResponse(basicResponse);
            return response;
        }

        /// <summary>
        /// Converts the basic push response.
        /// </summary>
        /// <param name="basicResponse">The basic response.</param>
        /// <returns></returns>
        private PushResponse ConvertBasicPushResponse(BasicPushResponse basicResponse)
        {
            PushResponse response = new PushResponse();
            response.Active = basicResponse.Active;
            if(basicResponse.Created != null)
            {
                response.Created = TimeZoneInfo.ConvertTime(basicResponse.Created.UnixTimeToDateTime(), TimeZoneInfo);
            }
            response.Dismissed = basicResponse.Dismissed;
            response.Direction = basicResponse.Direction;
            response.Iden = basicResponse.Iden;
            if(basicResponse.Modified != null)
            {
                response.Modified = TimeZoneInfo.ConvertTime(basicResponse.Modified.UnixTimeToDateTime(), TimeZoneInfo);
            }
            response.ReceiverEmail = basicResponse.ReceiverEmail;
            response.ReceiverEmailNormalized = basicResponse.ReceiverEmailNormalized;
            response.ReceiverIden = basicResponse.ReceiverIden;
            response.SenderEmail = basicResponse.SenderEmail;
            response.SenderEmailNormalized = basicResponse.SenderEmailNormalized;
            response.SenderIden = basicResponse.SenderIden;
            response.SenderName = basicResponse.SenderName;
            response.SourceDeviceIden = basicResponse.SourceDeviceIden;
            response.TargetDeviceIden = basicResponse.TargetDeviceIden;
            response.Type = ConvertPushResponseType(basicResponse.Type);
            response.ClientIden = basicResponse.ClientIden;
            response.Title = basicResponse.Title;
            response.Body = basicResponse.Body;
            response.Url = basicResponse.Url;
            response.FileName = basicResponse.FileName;
            response.FileType = basicResponse.FileType;
            response.FileUrl = basicResponse.FileUrl;
            response.ImageUrl = basicResponse.ImageUrl;
            response.Name = basicResponse.Name;
            return response;
        }

        /// <summary>
        /// Converts the type of the push response.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        private PushResponseType ConvertPushResponseType(string type)
        {
            switch(type)
            {
                case Constants.TypeConstants.File:
                    return PushResponseType.File;
                case Constants.TypeConstants.Link:
                    return PushResponseType.Link;
                case Constants.TypeConstants.Note:
                default:
                    return PushResponseType.Note;
            }
        }

        #endregion private methods

        public void Parse(PushBulletClientLite source)
        {
            AccessToken = source.AccessToken;
        }

        public void Init() { }

        public override int GetHashCode()
        {
            return AccessToken.GetHashCode() ^ TimeZoneInfo.GetSafeHashCode();
        }
    }
}