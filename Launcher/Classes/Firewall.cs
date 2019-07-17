using NetFwTypeLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Launcher.Classes
{
    sealed class Firewall
    {
        public static string InstallPath
        {
            get
            {
                StreamReader reader = new StreamReader("Credentials.xml", Encoding.UTF8);
                while (!reader.EndOfStream)
                {
                    string str = reader.ReadLine();
                    var match = Regex.Match(str, @"<GamePath>(.+)</GamePath>");
                    if (match.Success)
                    {
                        return match.Result("$1");
                    }
                }
                return string.Empty;
            }
        }

        public static void CrypticErrorDenyApps()
        {
            if (string.IsNullOrEmpty(InstallPath))
                return;

            var files = new DirectoryInfo(InstallPath).GetFiles("*.exe", SearchOption.AllDirectories).ToList()
                .Where(file => Regex.IsMatch(file.Name, @"(CrashReporter|CrypticError)", RegexOptions.IgnoreCase))
                .ToList();

            files.ForEach(file => Deny(file.FullName.Replace(InstallPath + "\\", "").Replace("\\", " : "), file.FullName));
        }

        public static void CrypticErrorDenyServer()
        {
            Deny("CrypticError server", "", "74.207.241.31");
            Deny("Game Error server", "", "208.95.187.80");
            Deny("TicketTracker server", "", "172.26.11.26");
        }

        public static void RemoveAllRules()
        {
            var rules = Rules.GetRulesByGrouping("CrypticError");
            rules.ForEach(r => Rules.Remove(r.Name));
        }

        private static bool Deny(string name, string appPath, string remoteAddress = "")
        {
            return Rules.Add
                (
                name: name,
                description: "",
                applicationName: appPath,
                serviceName: "",
                protocol: (int)Rules.EFirewallProtocol.Any,
                localPorts: "",
                remotePorts: "",
                localAddresses: "",
                remoteAddresses: remoteAddress,
                IcmpTypesAndCodes: "",
                direction: Rules.EFirewallRuleDirection.Out,
                interfaces: null,
                interfaceTypes: "",
                enabled: true,
                grouping: "CrypticError",
                profiles: (int)Rules.EFirewallProfiles.All,
                edgeTraversal: false,
                action: Rules.EFirewallRuleAction.Block,
                edgeTraversalOptions: 0
                );
        }

        private struct App
        {
            public string Name { get; }
            public string Path { get; }
            public App(string path)
            {
                Name = path.Split(new char[2] { '\\', '/' }).Last();
                Path = path;
            }
        }

        /// <summary>
        /// Provides static methods to add, remove and check the existance of, Windows firewall rules
        /// </summary>
        private static class Rules
        {
            /// <summary>
            /// Create a firewall rule for ports
            /// </summary>
            /// <param name="ruleName">The name of the rule to add</param>
            /// <param name="ruleGroup">The group under which the rule is added</param>
            /// <param name="protocol">The desired rule protocol</param>
            /// <param name="localPorts">The desired rule port</param>
            /// <param name="action">The desired rule action, to allow or block communications</param>
            /// <param name="profiles">The desired rule profile</param>
            /// <returns>True if the rule was created, False if it already exists</returns>
            public static bool Add(string ruleName, string ruleGroup, EFirewallProtocol protocol = EFirewallProtocol.Tcp, string localPorts = "80", EFirewallRuleAction action = EFirewallRuleAction.Allowed, EFirewallProfiles profiles = EFirewallProfiles.All)
            {
                if (Exists(ruleName))
                {
                    return false;
                }
                INetFwPolicy2 netFwPolicy = (INetFwPolicy2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwPolicy2"));
                int currentProfileTypes = netFwPolicy.CurrentProfileTypes;
                INetFwRule2 netFwRule = (INetFwRule2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FWRule"));
                netFwRule.Enabled = true;
                netFwRule.Action = (NET_FW_ACTION_)action;
                netFwRule.Protocol = (int)protocol;
                netFwRule.LocalPorts = localPorts;
                netFwRule.Name = ruleName;
                netFwRule.Grouping = ruleGroup;
                netFwRule.Profiles = (int)profiles;
                netFwPolicy.Rules.Add(netFwRule);
                return true;
            }

            /// <summary>
            /// Create a firewall rule for application
            /// </summary>
            /// <param name="name">The name of the firewall rule</param>
            /// <param name="description">The description</param>
            /// <param name="applicationName">The application name</param>
            /// <param name="serviceName">The service name</param>
            /// <param name="protocol">The  protocol</param>
            /// <param name="localPorts">The local port(s)</param>
            /// <param name="remotePorts">The remote port(s)</param>
            /// <param name="localAddresses">The local address(es)</param>
            /// <param name="remoteAddresses">The remote address(es)</param>
            /// <param name="IcmpTypesAndCodes">IcmpTypesAndCodes</param>
            /// <param name="direction">The rule direction</param>
            /// <param name="interfaces">interfaces</param>
            /// <param name="interfaceTypes">interfaceTypes</param>
            /// <param name="enabled">Whether the rule is enabled or not</param>
            /// <param name="grouping">grouping</param>
            /// <param name="profiles">profiles</param>
            /// <param name="edgeTraversal">edgeTraversal</param>
            /// <param name="action">action</param>
            /// <param name="edgeTraversalOptions">edgeTraversalOptions</param>
            /// <returns>True if the rule was created, False if it already exists</returns>
            /// <remarks>I have not tested all scenarios.  Please reports any issues.</remarks>
            public static bool Add(string name, string description, string applicationName, string serviceName, int protocol, string localPorts, string remotePorts, string localAddresses, string remoteAddresses, string IcmpTypesAndCodes, EFirewallRuleDirection direction, object interfaces, string interfaceTypes, bool enabled, string grouping, int profiles, bool edgeTraversal, EFirewallRuleAction action, int edgeTraversalOptions)
            {
                if (Exists(name))
                {
                    return false;
                }
                INetFwPolicy2 netFwPolicy = (INetFwPolicy2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwPolicy2"));
                int currentProfileTypes = netFwPolicy.CurrentProfileTypes;
                INetFwRule2 netFwRule = (INetFwRule2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FWRule"));
                netFwRule.Name = name;
                if (!string.IsNullOrEmpty(description))
                {
                    netFwRule.Description = description;
                }
                if (!string.IsNullOrEmpty(applicationName))
                {
                    netFwRule.ApplicationName = applicationName;
                }
                if (!string.IsNullOrEmpty(serviceName))
                {
                    netFwRule.serviceName = serviceName;
                }
                netFwRule.Protocol = protocol;
                if (!string.IsNullOrEmpty(localPorts))
                {
                    netFwRule.LocalPorts = localPorts;
                }
                if (!string.IsNullOrEmpty(remotePorts))
                {
                    netFwRule.RemotePorts = remotePorts;
                }
                if (!string.IsNullOrEmpty(localAddresses))
                {
                    netFwRule.LocalAddresses = localAddresses;
                }
                if (!string.IsNullOrEmpty(remoteAddresses))
                {
                    netFwRule.RemoteAddresses = remoteAddresses;
                }
                if (!string.IsNullOrEmpty(IcmpTypesAndCodes))
                {
                    netFwRule.IcmpTypesAndCodes = IcmpTypesAndCodes;
                }
                netFwRule.Direction = (NET_FW_RULE_DIRECTION_)direction;
                if (interfaces != null)
                {
                    netFwRule.Interfaces = interfaces;
                }
                if (!string.IsNullOrEmpty(interfaceTypes))
                {
                    netFwRule.InterfaceTypes = interfaceTypes;
                }
                netFwRule.Enabled = enabled;
                if (!string.IsNullOrEmpty(grouping))
                {
                    netFwRule.Grouping = grouping;
                }
                netFwRule.Profiles = profiles;
                netFwRule.EdgeTraversal = edgeTraversal;
                netFwRule.Action = (NET_FW_ACTION_)action;
                netFwRule.EdgeTraversalOptions = edgeTraversalOptions;
                netFwPolicy.Rules.Add(netFwRule);
                return true;
            }

            /// <summary>
            /// Get a list of rules by specified Grouping
            /// </summary>
            /// <param name="groupName">The name of the rule to check</param>
            /// <returns>List of INetFwRule</returns>
            public static List<INetFwRule> GetRulesByGrouping(string groupName)
            {
                List<INetFwRule> rules = new List<INetFwRule>();
                foreach (INetFwRule rule in ((INetFwPolicy2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwPolicy2"))).Rules)
                {
                    if (rule.Grouping == groupName)
                    {
                        rules.Add(rule);
                    }
                }
                return rules;
            }

            /// <summary>
            /// Checks if a particular rule exists
            /// </summary>
            /// <param name="ruleName">The name of the rule to check</param>
            /// <returns>True if the rule exists, otherwise false</returns>
            public static bool Exists(string ruleName)
            {
                bool result = false;
                foreach (INetFwRule rule in ((INetFwPolicy2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwPolicy2"))).Rules)
                {
                    if (rule.Name == ruleName)
                    {
                        result = true;
                        break;
                    }
                }
                return result;
            }

            /// <summary>
            /// Removes a firewall rule
            /// </summary>
            /// <param name="ruleName">The name of the rule to remove</param>
            /// <returns>True if the rule exists and was removed, otherwise False</returns>
            public static bool Remove(string ruleName)
            {
                if (Exists(ruleName))
                {
                    ((INetFwPolicy2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwPolicy2"))).Rules.Remove(ruleName);
                    return true;
                }
                return false;
            }

            /// <summary>
            /// Firewall protocols
            /// </summary>
            public enum EFirewallProtocol
            {
                /// <summary>
                /// Any
                /// </summary>
                Any = 256,
                /// <summary>
                /// Tcp
                /// </summary>
                Tcp = 6,
                /// <summary>
                /// Udp
                /// </summary>
                Udp = 17
            }

            /// <summary>
            /// Firewall rule actions
            /// </summary>
            public enum EFirewallRuleAction
            {
                /// <summary>
                /// Allow communications
                /// </summary>
                Allowed = 1,
                /// <summary>
                /// Block communications
                /// </summary>
                Block = 0,
                /// <summary>
                /// Max
                /// </summary>
                Max = 2
            }

            /// <summary>
            /// The firewall profile type
            /// </summary>
            public enum EFirewallProfiles
            {
                /// <summary>
                /// Public
                /// </summary>
                Public = 4,
                /// <summary>
                /// Private
                /// </summary>
                Private = 2,
                /// <summary>
                /// Domain
                /// </summary>
                Domain = 1,
                /// <summary>
                /// All
                /// </summary>
                All = 2147483647
            }

            /// <summary>
            /// Firewall rule direction
            /// </summary>
            public enum EFirewallRuleDirection
            {
                /// <summary>
                /// Inbound rule
                /// </summary>
                In = 1,
                /// <summary>
                /// Outbound rule
                /// </summary>
                Out,
                /// <summary>
                /// Max
                /// </summary>
                Max
            }
        }
    }
}
