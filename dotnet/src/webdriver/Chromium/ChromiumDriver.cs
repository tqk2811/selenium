// <copyright file="ChromiumDriver.cs" company="WebDriver Committers">
// Licensed to the Software Freedom Conservancy (SFC) under one
// or more contributor license agreements. See the NOTICE file
// distributed with this work for additional information
// regarding copyright ownership. The SFC licenses this file
// to you under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using OpenQA.Selenium.DevTools;
using OpenQA.Selenium.Remote;

namespace OpenQA.Selenium.Chromium
{
    /// <summary>
    /// Provides an abstract way to access Chromium-based browsers to run tests.
    /// </summary>
    public class ChromiumDriver : WebDriver, ISupportsLogs, IDevTools
    {
        /// <summary>
        /// Accept untrusted SSL Certificates
        /// </summary>
        public static readonly bool AcceptUntrustedCertificates = true;

        /// <summary>
        /// Command for executing a Chrome DevTools Protocol command in a driver for a Chromium-based browser.
        /// </summary>
        public static readonly string ExecuteCdp = "executeCdpCommand";

        /// <summary>
        /// Command for getting cast sinks in a driver for a Chromium-based browser.
        /// </summary>
        public static readonly string GetCastSinksCommand = "getCastSinks";

        /// <summary>
        /// Command for selecting a cast sink in a driver for a Chromium-based browser.
        /// </summary>
        public static readonly string SelectCastSinkCommand = "selectCastSink";

        /// <summary>
        /// Command for starting cast tab mirroring in a driver for a Chromium-based browser.
        /// </summary>
        public static readonly string StartCastTabMirroringCommand = "startCastTabMirroring";

        /// <summary>
        /// Command for starting cast desktop mirroring in a driver for a Chromium-based browser.
        /// </summary>
        public static readonly string StartCastDesktopMirroringCommand = "startCastDesktopMirroring";

        /// <summary>
        /// Command for getting a cast issued message in a driver for a Chromium-based browser.
        /// </summary>
        public static readonly string GetCastIssueMessageCommand = "getCastIssueMessage";

        /// <summary>
        /// Command for stopping casting in a driver for a Chromium-based browser.
        /// </summary>
        public static readonly string StopCastingCommand = "stopCasting";

        /// <summary>
        /// Command for getting the simulated network conditions in a driver for a Chromium-based browser.
        /// </summary>
        public static readonly string GetNetworkConditionsCommand = "getNetworkConditions";

        /// <summary>
        /// Command for setting the simulated network conditions in a driver for a Chromium-based browser.
        /// </summary>
        public static readonly string SetNetworkConditionsCommand = "setNetworkConditions";

        /// <summary>
        /// Command for deleting the simulated network conditions in a driver for a Chromium-based browser.
        /// </summary>
        public static readonly string DeleteNetworkConditionsCommand = "deleteNetworkConditions";

        /// <summary>
        /// Command for executing a Chrome DevTools Protocol command in a driver for a Chromium-based browser.
        /// </summary>
        public static readonly string SendChromeCommand = "sendChromeCommand";

        /// <summary>
        /// Command for executing a Chrome DevTools Protocol command that returns a result in a driver for a Chromium-based browser.
        /// </summary>
        public static readonly string SendChromeCommandWithResult = "sendChromeCommandWithResult";

        /// <summary>
        /// Command for launching an app in a driver for a Chromium-based browser.
        /// </summary>
        public static readonly string LaunchAppCommand = "launchAppCommand";

        /// <summary>
        /// Command for setting permissions in a driver for a Chromium-based browser.
        /// </summary>
        public static readonly string SetPermissionCommand = "setPermission";

        private readonly string optionsCapabilityName;
        private DevToolsSession devToolsSession;

        private static Dictionary<string, CommandInfo> chromiumCustomCommands = new Dictionary<string, CommandInfo>()
        {
            { GetNetworkConditionsCommand, new HttpCommandInfo(HttpCommandInfo.GetCommand, "/session/{sessionId}/chromium/network_conditions") },
            { SetNetworkConditionsCommand, new HttpCommandInfo(HttpCommandInfo.PostCommand, "/session/{sessionId}/chromium/network_conditions") },
            { DeleteNetworkConditionsCommand, new HttpCommandInfo(HttpCommandInfo.DeleteCommand, "/session/{sessionId}/chromium/network_conditions") },
            { SendChromeCommand, new HttpCommandInfo(HttpCommandInfo.PostCommand, "/session/{sessionId}/chromium/send_command") },
            { SendChromeCommandWithResult, new HttpCommandInfo(HttpCommandInfo.PostCommand, "/session/{sessionId}/chromium/send_command_and_get_result") },
            { LaunchAppCommand, new HttpCommandInfo(HttpCommandInfo.PostCommand, "/session/{sessionId}/chromium/launch_app") },
            { SetPermissionCommand, new HttpCommandInfo(HttpCommandInfo.PostCommand, "/session/{sessionId}/permissions") }
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="ChromiumDriver"/> class using the specified <see cref="ChromiumDriverService"/>.
        /// </summary>
        /// <param name="service">The <see cref="ChromiumDriverService"/> to use.</param>
        /// <param name="options">The <see cref="ChromiumOptions"/> to be used with the ChromiumDriver.</param>
        /// <param name="commandTimeout">The maximum amount of time to wait for each command.</param>
        protected ChromiumDriver(ChromiumDriverService service, ChromiumOptions options, TimeSpan commandTimeout)
            : base(new DriverServiceCommandExecutor(service, commandTimeout), ConvertOptionsToCapabilities(options))
        {
            this.optionsCapabilityName = options.CapabilityName;
        }

        protected static IReadOnlyDictionary<string, CommandInfo> ChromiumCustomCommands
        {
            get { return new ReadOnlyDictionary<string, CommandInfo>(chromiumCustomCommands); }
        }

        /// <summary>
        /// Gets or sets the <see cref="IFileDetector"/> responsible for detecting
        /// sequences of keystrokes representing file paths and names.
        /// </summary>
        /// <remarks>The Chromium driver does not allow a file detector to be set,
        /// as the server component of the Chromium driver only
        /// allows uploads from the local computer environment. Attempting to set
        /// this property has no effect, but does not throw an exception. If you
        /// are attempting to run the Chromium driver remotely, use <see cref="RemoteWebDriver"/>
        /// in conjunction with a standalone WebDriver server.</remarks>
        public override IFileDetector FileDetector
        {
            get { return base.FileDetector; }
            set { }
        }

        /// <summary>
        /// Gets a value indicating whether a DevTools session is active.
        /// </summary>
        public bool HasActiveDevToolsSession
        {
            get { return this.devToolsSession != null; }
        }

        /// <summary>
        /// Gets or sets the network condition emulation for Chromium.
        /// </summary>
        public ChromiumNetworkConditions NetworkConditions
        {
            get
            {
                Response response = this.ExecuteAsync(GetNetworkConditionsCommand, null).ConfigureAwait(false).GetAwaiter().GetResult();
                return ChromiumNetworkConditions.FromDictionary(response.Value as Dictionary<string, object>);
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value), "value must not be null");
                }

                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters["network_conditions"] = value;
                this.ExecuteAsync(SetNetworkConditionsCommand, parameters).ConfigureAwait(false).GetAwaiter().GetResult();
            }
        }

        /// <summary>
        /// Launches a Chromium based application.
        /// </summary>
        /// <param name="id">ID of the chromium app to launch.</param>
        public void LaunchApp(string id)
            => LaunchAppAsync(id).ConfigureAwait(false).GetAwaiter().GetResult();

        /// <summary>
        /// Launches a Chromium based application.
        /// </summary>
        /// <param name="id">ID of the chromium app to launch.</param>
        public Task LaunchAppAsync(string id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id), "id must not be null");
            }

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters["id"] = id;
            return this.ExecuteAsync(LaunchAppCommand, parameters);
        }

        /// <summary>
        /// Set supported permission on browser.
        /// </summary>
        /// <param name="permissionName">Name of item to set the permission on.</param>
        /// <param name="permissionValue">Value to set the permission to.</param>
        public void SetPermission(string permissionName, string permissionValue)
            => SetPermissionAsync(permissionName, permissionValue).ConfigureAwait(false).GetAwaiter().GetResult();

        /// <summary>
        /// Set supported permission on browser.
        /// </summary>
        /// <param name="permissionName">Name of item to set the permission on.</param>
        /// <param name="permissionValue">Value to set the permission to.</param>
        public Task SetPermissionAsync(string permissionName, string permissionValue)
        {
            if (permissionName == null)
            {
                throw new ArgumentNullException(nameof(permissionName), "name must not be null");
            }

            if (permissionValue == null)
            {
                throw new ArgumentNullException(nameof(permissionValue), "value must not be null");
            }

            Dictionary<string, object> nameParameter = new Dictionary<string, object>();
            nameParameter["name"] = permissionName;
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters["descriptor"] = nameParameter;
            parameters["state"] = permissionValue;
            return this.ExecuteAsync(SetPermissionCommand, parameters);
        }

        /// <summary>
        /// Executes a custom Chrome Dev Tools Protocol Command.
        /// </summary>
        /// <param name="commandName">Name of the command to execute.</param>
        /// <param name="commandParameters">Parameters of the command to execute.</param>
        /// <returns>An object representing the result of the command, if applicable.</returns>
        public object ExecuteCdpCommand(string commandName, Dictionary<string, object> commandParameters)
            => ExecuteCdpCommandAsync(commandName, commandParameters).ConfigureAwait(false).GetAwaiter().GetResult();

        /// <summary>
        /// Executes a custom Chrome Dev Tools Protocol Command.
        /// </summary>
        /// <param name="commandName">Name of the command to execute.</param>
        /// <param name="commandParameters">Parameters of the command to execute.</param>
        /// <returns>An object representing the result of the command, if applicable.</returns>
        public async Task<object> ExecuteCdpCommandAsync(string commandName, Dictionary<string, object> commandParameters)
        {
            if (commandName == null)
            {
                throw new ArgumentNullException(nameof(commandName), "commandName must not be null");
            }

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters["cmd"] = commandName;
            parameters["params"] = commandParameters;
            Response response = await this.ExecuteAsync(ExecuteCdp, parameters).ConfigureAwait(false);
            return response.Value;
        }

        /// <summary>
        /// Creates a session to communicate with a browser using the Chromium Developer Tools debugging protocol.
        /// </summary>
        /// <param name="devToolsProtocolVersion">The version of the Chromium Developer Tools protocol to use. Defaults to autodetect the protocol version.</param>
        /// <returns>The active session to use to communicate with the Chromium Developer Tools debugging protocol.</returns>
        public Task<DevToolsSession> GetDevToolsSessionAsync()
        {
            return GetDevToolsSessionAsync(DevToolsSession.AutoDetectDevToolsProtocolVersion);
        }

        /// <summary>
        /// Creates a session to communicate with a browser using the Chromium Developer Tools debugging protocol.
        /// </summary>
        /// <param name="devToolsProtocolVersion">The version of the Chromium Developer Tools protocol to use. Defaults to autodetect the protocol version.</param>
        /// <returns>The active session to use to communicate with the Chromium Developer Tools debugging protocol.</returns>
        public async Task<DevToolsSession> GetDevToolsSessionAsync(int devToolsProtocolVersion)
        {
            if (this.devToolsSession == null)
            {
                if (!this.Capabilities.HasCapability(this.optionsCapabilityName))
                {
                    throw new WebDriverException("Cannot find " + this.optionsCapabilityName + " capability for driver");
                }

                Dictionary<string, object> options = this.Capabilities.GetCapability(this.optionsCapabilityName) as Dictionary<string, object>;
                if (options == null)
                {
                    throw new WebDriverException("Found " + this.optionsCapabilityName + " capability, but is not an object");
                }

                if (!options.ContainsKey("debuggerAddress"))
                {
                    throw new WebDriverException("Did not find debuggerAddress capability in " + this.optionsCapabilityName);
                }

                string debuggerAddress = options["debuggerAddress"].ToString();
                try
                {
                    DevToolsSession session = new DevToolsSession(debuggerAddress);
                    await session.StartSession(devToolsProtocolVersion).ConfigureAwait(false);
                    this.devToolsSession = session;
                }
                catch (Exception e)
                {
                    throw new WebDriverException("Unexpected error creating WebSocket DevTools session.", e);
                }
            }

            return this.devToolsSession;
        }

        /// <summary>
        /// Closes a DevTools session.
        /// </summary>
        public async Task CloseDevToolsSessionAsync()
        {
            if (this.devToolsSession != null)
            {
                await this.devToolsSession.StopSession(true).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Clears simulated network conditions.
        /// </summary>
        public void ClearNetworkConditions()
            => ClearNetworkConditionsAsync().ConfigureAwait(false).GetAwaiter().GetResult();

        /// <summary>
        /// Clears simulated network conditions.
        /// </summary>
        public Task ClearNetworkConditionsAsync()
        {
            return this.ExecuteAsync(DeleteNetworkConditionsCommand, null);
        }

        /// <summary>
        /// Returns the list of cast sinks (Cast devices) available to the Chrome media router.
        /// </summary>
        /// <returns>The list of available sinks.</returns>
        public List<Dictionary<string, string>> GetCastSinks()
            => GetCastSinksAsync().ConfigureAwait(false).GetAwaiter().GetResult();

        /// <summary>
        /// Returns the list of cast sinks (Cast devices) available to the Chrome media router.
        /// </summary>
        /// <returns>The list of available sinks.</returns>
        public async Task<List<Dictionary<string, string>>> GetCastSinksAsync()
        {
            List<Dictionary<string, string>> returnValue = new List<Dictionary<string, string>>();
            Response response = await this.ExecuteAsync(GetCastSinksCommand, null).ConfigureAwait(false);
            object[] responseValue = response.Value as object[];
            if (responseValue != null)
            {
                foreach (object entry in responseValue)
                {
                    Dictionary<string, object> entryValue = entry as Dictionary<string, object>;
                    if (entryValue != null)
                    {
                        Dictionary<string, string> sink = new Dictionary<string, string>();
                        foreach (KeyValuePair<string, object> pair in entryValue)
                        {
                            sink[pair.Key] = pair.Value.ToString();
                        }

                        returnValue.Add(sink);
                    }
                }
            }
            return returnValue;
        }

        /// <summary>
        /// Selects a cast sink (Cast device) as the recipient of media router intents (connect or play).
        /// </summary>
        /// <param name="deviceName">Name of the target sink (device).</param>
        public void SelectCastSink(string deviceName)
            => SelectCastSinkAsync(deviceName).ConfigureAwait(false).GetAwaiter().GetResult();

        /// <summary>
        /// Selects a cast sink (Cast device) as the recipient of media router intents (connect or play).
        /// </summary>
        /// <param name="deviceName">Name of the target sink (device).</param>
        public Task SelectCastSinkAsync(string deviceName)
        {
            if (deviceName == null)
            {
                throw new ArgumentNullException(nameof(deviceName), "deviceName must not be null");
            }

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters["sinkName"] = deviceName;
            return this.ExecuteAsync(SelectCastSinkCommand, parameters);
        }

        /// <summary>
        /// Initiates tab mirroring for the current browser tab on the specified device.
        /// </summary>
        /// <param name="deviceName">Name of the target sink (device).</param>
        public void StartTabMirroring(string deviceName)
            => StartTabMirroringAsync(deviceName).ConfigureAwait(false).GetAwaiter().GetResult();

        /// <summary>
        /// Initiates tab mirroring for the current browser tab on the specified device.
        /// </summary>
        /// <param name="deviceName">Name of the target sink (device).</param>
        public Task StartTabMirroringAsync(string deviceName)
        {
            if (deviceName == null)
            {
                throw new ArgumentNullException(nameof(deviceName), "deviceName must not be null");
            }

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters["sinkName"] = deviceName;
            return this.ExecuteAsync(StartCastTabMirroringCommand, parameters);
        }

        /// <summary>
        /// Initiates mirroring of the desktop on the specified device.
        /// </summary>
        /// <param name="deviceName">Name of the target sink (device).</param>
        public void StartDesktopMirroring(string deviceName)
            => StartDesktopMirroringAsync(deviceName).ConfigureAwait(false).GetAwaiter().GetResult();

        /// <summary>
        /// Initiates mirroring of the desktop on the specified device.
        /// </summary>
        /// <param name="deviceName">Name of the target sink (device).</param>
        public Task StartDesktopMirroringAsync(string deviceName)
        {
            if (deviceName == null)
            {
                throw new ArgumentNullException(nameof(deviceName), "deviceName must not be null");
            }

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters["sinkName"] = deviceName;
            return this.ExecuteAsync(StartCastDesktopMirroringCommand, parameters);
        }

        /// <summary>
        /// Returns the error message if there is any issue in a Cast session.
        /// </summary>
        /// <returns>An error message.</returns>
        public string GetCastIssueMessage()
            => GetCastIssueMessageAsync().ConfigureAwait(false).GetAwaiter().GetResult();

        /// <summary>
        /// Returns the error message if there is any issue in a Cast session.
        /// </summary>
        /// <returns>An error message.</returns>
        public async Task<string> GetCastIssueMessageAsync()
        {
            Response response = await this.ExecuteAsync(GetCastIssueMessageCommand, null).ConfigureAwait(false);
            return (string)response.Value;
        }

        /// <summary>
        /// Stops casting from media router to the specified device, if connected.
        /// </summary>
        /// <param name="deviceName">Name of the target sink (device).</param>
        public void StopCasting(string deviceName)
            => StopCastingAsync(deviceName).ConfigureAwait(false).GetAwaiter().GetResult();

        /// <summary>
        /// Stops casting from media router to the specified device, if connected.
        /// </summary>
        /// <param name="deviceName">Name of the target sink (device).</param>
        public Task StopCastingAsync(string deviceName)
        {
            if (deviceName == null)
            {
                throw new ArgumentNullException(nameof(deviceName), "deviceName must not be null");
            }

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters["sinkName"] = deviceName;
            return this.ExecuteAsync(StopCastingCommand, parameters);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.devToolsSession != null)
                {
                    this.devToolsSession.Dispose();
                    this.devToolsSession = null;
                }
            }

            base.Dispose(disposing);
        }

        private static ICapabilities ConvertOptionsToCapabilities(ChromiumOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options), "options must not be null");
            }

            return options.ToCapabilities();
        }
    }
}
