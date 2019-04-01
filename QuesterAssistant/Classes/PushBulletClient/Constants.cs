namespace QuesterAssistant.Classes.PushBulletClient
{
    public class Constants
    {
        public const string BaseUrl = "https://api.pushbullet.com/v2/";
        public const string BaseUrlNonVersion = "https://api.pushbullet.com/";
        public class StatusCodeExceptions
        {
            public const string BadRequest = "400 Bad Request - Usually this results from missing a required parameter.";
            public const string Unauthorized = "401 Unauthorized - No valid access token provided.";
            public const string Forbidden = "403 Forbidden - The access token is not valid for that request.";
            public const string NotFound = "404 Not Found - The requested item doesn't exist.";
            public const string TooManyRequests = "429 Too Many Requests - You have been ratelimited for making too many requests to the server.";
            public const string FiveHundredXX = "{0} {1} - Something went wrong on Pushbullet's side.";
            public const string Default = "{0} {1} - Error getting data from Pushbullet.";
        }
        public class PushRequestErrorMessages
        {
            public const string EmptyDeviceIdenProperty = "The device iden property for the reqeust is empty.";
            public const string EmptyTypeProperty = "The type property for the reqeust is empty. Not even sure how that happened.";
            public const string EmptyEmailProperty = "The email property for the request is empty. This is only a problem because both the device iden and client iden were empty, too.";
        }
        public class PushNoteRequestErrorMessages
        {
            public const string EmptyTitleProperty = "The title property for the note request is empty. Please provide a title.";
            public const string EmptyBodyProperty = "The body property for the note request is empty. Please provide a body.";
        }
        public class HeadersConstants
        {
            public const string AuthorizationKey = "Authorization";
            public const string AuthorizationValue = "Bearer {0}";
        }
        public class TypeConstants
        {
            public const string Note = "note";
            public const string Link = "link";
            public const string File = "file";
        }
        public class UsersUrls
        {
            public const string Me = "users/me";
        }
        public class DevicesUrls
        {
            public const string Me = "devices";
        }
        public class PushesUrls
        {
            public const string Pushes = "pushes";
        }
        public class MimeTypes
        {
            public const string Json = "application/json";
            public const string OctetStream = "application/octet-stream";
            public const string FormUrlEncoded = "application/x-www-form-urlencoded";
        }
    }
}