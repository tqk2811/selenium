// <copyright file="SynchronouslyWrapperExtensions.cs" company="WebDriver Committers">
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
using OpenQA.Selenium.Chromium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Safari;
using OpenQA.Selenium.DevTools;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Internal;
using OpenQA.Selenium.VirtualAuth;

namespace OpenQA.Selenium
{
    public static class SynchronouslyWrapperExtensions
    {
        #region ChromiumDriver

        /// <summary>
        /// Launches a Chromium based application.
        /// </summary>
        /// <param name="id">ID of the chromium app to launch.</param>
        public static void LaunchApp(this ChromiumDriver chromiumDriver, string id)
            => chromiumDriver.LaunchAppAsync(id).ConfigureAwait(false).GetAwaiter().GetResult();

        /// <summary>
        /// Set supported permission on browser.
        /// </summary>
        /// <param name="permissionName">Name of item to set the permission on.</param>
        /// <param name="permissionValue">Value to set the permission to.</param>
        public static void SetPermission(this ChromiumDriver chromiumDriver, string permissionName, string permissionValue)
            => chromiumDriver.SetPermissionAsync(permissionName, permissionValue).ConfigureAwait(false).GetAwaiter().GetResult();

        /// <summary>
        /// Executes a custom Chrome Dev Tools Protocol Command.
        /// </summary>
        /// <param name="commandName">Name of the command to execute.</param>
        /// <param name="commandParameters">Parameters of the command to execute.</param>
        /// <returns>An object representing the result of the command, if applicable.</returns>
        public static object ExecuteCdpCommand(this ChromiumDriver chromiumDriver, string commandName, Dictionary<string, object> commandParameters)
            => chromiumDriver.ExecuteCdpCommandAsync(commandName, commandParameters).ConfigureAwait(false).GetAwaiter().GetResult();

        /// <summary>
        /// Clears simulated network conditions.
        /// </summary>
        public static void ClearNetworkConditions(this ChromiumDriver chromiumDriver)
            => chromiumDriver.ClearNetworkConditionsAsync().ConfigureAwait(false).GetAwaiter().GetResult();

        /// <summary>
        /// Returns the list of cast sinks (Cast devices) available to the Chrome media router.
        /// </summary>
        /// <returns>The list of available sinks.</returns>
        public static List<Dictionary<string, string>> GetCastSinks(this ChromiumDriver chromiumDriver)
            => chromiumDriver.GetCastSinksAsync().ConfigureAwait(false).GetAwaiter().GetResult();

        /// <summary>
        /// Selects a cast sink (Cast device) as the recipient of media router intents (connect or play).
        /// </summary>
        /// <param name="deviceName">Name of the target sink (device).</param>
        public static void SelectCastSink(this ChromiumDriver chromiumDriver, string deviceName)
            => chromiumDriver.SelectCastSinkAsync(deviceName).ConfigureAwait(false).GetAwaiter().GetResult();

        /// <summary>
        /// Initiates tab mirroring for the current browser tab on the specified device.
        /// </summary>
        /// <param name="deviceName">Name of the target sink (device).</param>
        public static void StartTabMirroring(this ChromiumDriver chromiumDriver, string deviceName)
            => chromiumDriver.StartTabMirroringAsync(deviceName).ConfigureAwait(false).GetAwaiter().GetResult();

        /// <summary>
        /// Initiates mirroring of the desktop on the specified device.
        /// </summary>
        /// <param name="deviceName">Name of the target sink (device).</param>
        public static void StartDesktopMirroring(this ChromiumDriver chromiumDriver, string deviceName)
            => chromiumDriver.StartDesktopMirroringAsync(deviceName).ConfigureAwait(false).GetAwaiter().GetResult();

        /// <summary>
        /// Returns the error message if there is any issue in a Cast session.
        /// </summary>
        /// <returns>An error message.</returns>
        public static string GetCastIssueMessage(this ChromiumDriver chromiumDriver)
            => chromiumDriver.GetCastIssueMessageAsync().ConfigureAwait(false).GetAwaiter().GetResult();

        /// <summary>
        /// Stops casting from media router to the specified device, if connected.
        /// </summary>
        /// <param name="deviceName">Name of the target sink (device).</param>
        public static void StopCasting(this ChromiumDriver chromiumDriver, string deviceName)
            => chromiumDriver.StopCastingAsync(deviceName).ConfigureAwait(false).GetAwaiter().GetResult();

        #endregion ChromiumDriver


        #region FirefoxDriver

        /// <summary>
        /// Sets the command context used when issuing commands to geckodriver.
        /// </summary>
        /// <exception cref="WebDriverException">If response is not recognized</exception>
        /// <returns>The context of commands.</returns>
        public static FirefoxCommandContext GetContext(this FirefoxDriver firefoxDriver)
            => firefoxDriver.GetContextAsync().ConfigureAwait(false).GetAwaiter().GetResult();

        /// <summary>
        /// Sets the command context used when issuing commands to geckodriver.
        /// </summary>
        /// <param name="context">The <see cref="FirefoxCommandContext"/> value to which to set the context.</param>
        public static void SetContext(this FirefoxDriver firefoxDriver, FirefoxCommandContext context)
            => firefoxDriver.SetContextAsync(context).ConfigureAwait(false).GetAwaiter().GetResult();

        /// <summary>
        /// Installs a Firefox add-on from a directory.
        /// </summary>
        /// <param name="addOnDirectoryToInstall">Full path of the directory of the add-on to install.</param>
        /// <param name="temporary">Whether the add-on is temporary; required for unsigned add-ons.</param>
        public static string InstallAddOnFromDirectory(this FirefoxDriver firefoxDriver, string addOnDirectoryToInstall, bool temporary = false)
            => firefoxDriver.InstallAddOnFromDirectoryAsync(addOnDirectoryToInstall, temporary).ConfigureAwait(false).GetAwaiter().GetResult();

        /// <summary>
        /// Installs a Firefox add-on from a file, typically a .xpi file.
        /// </summary>
        /// <param name="addOnFileToInstall">Full path and file name of the add-on to install.</param>
        /// <param name="temporary">Whether the add-on is temporary; required for unsigned add-ons.</param>
        public static string InstallAddOnFromFile(this FirefoxDriver firefoxDriver, string addOnFileToInstall, bool temporary = false)
            => firefoxDriver.InstallAddOnFromFileAsync(addOnFileToInstall, temporary).ConfigureAwait(false).GetAwaiter().GetResult();

        /// <summary>
        /// Installs a Firefox add-on.
        /// </summary>
        /// <param name="base64EncodedAddOn">The base64-encoded string representation of the add-on binary.</param>
        /// <param name="temporary">Whether the add-on is temporary; required for unsigned add-ons.</param>
        public static string InstallAddOn(this FirefoxDriver firefoxDriver, string base64EncodedAddOn, bool temporary = false)
            => firefoxDriver.InstallAddOnAsync(base64EncodedAddOn, temporary).ConfigureAwait(false).GetAwaiter().GetResult();

        /// <summary>
        /// Uninstalls a Firefox add-on.
        /// </summary>
        /// <param name="addOnId">The ID of the add-on to uninstall.</param>
        public static void UninstallAddOn(this FirefoxDriver firefoxDriver, string addOnId)
            => firefoxDriver.UninstallAddOnAsync(addOnId).ConfigureAwait(false).GetAwaiter().GetResult();

        /// <summary>
        /// Gets a <see cref="Screenshot"/> object representing the image of the full page on the screen.
        /// </summary>
        /// <returns>A <see cref="Screenshot"/> object containing the image.</returns>
        public static Screenshot GetFullPageScreenshot(this FirefoxDriver firefoxDriver)
            => firefoxDriver.GetFullPageScreenshotAsync().ConfigureAwait(false).GetAwaiter().GetResult();

        #endregion FirefoxDriver


        #region SafariDriver

        /// <summary>
        /// This opens Safari's Web Inspector.
        /// If driver subsequently executes script of "debugger;"
        /// the execution will pause, no additional commands will be processed, and the code will time out.
        /// </summary>
        public static void AttachDebugger(this SafariDriver safariDriver)
            => safariDriver.AttachDebuggerAsync().ConfigureAwait(false).GetAwaiter().GetResult();

        /// <summary>
        /// Set permission of an item on the browser. The only supported permission at this time is "getUserMedia".
        /// </summary>
        /// <param name="permissionName">The name of the item to set permission on.</param>
        /// <param name="permissionValue">Whether the permission has been granted.</param>
        public static void SetPermission(this SafariDriver safariDriver, string permissionName, bool permissionValue)
            => safariDriver.SetPermissionAsync(permissionName, permissionValue).ConfigureAwait(false).GetAwaiter().GetResult();

        /// <summary>
        /// Returns Each available permission item and whether it is allowed or not.
        /// </summary>
        /// <returns>whether the item is allowed or not.</returns>
        public static object GetPermissions(this SafariDriver safariDriver)
            => safariDriver.GetPermissionsAsync().ConfigureAwait(false).GetAwaiter().GetResult();

        #endregion SafariDriver



        #region By

        /// <summary>
        /// Finds the first element matching the criteria.
        /// </summary>
        /// <param name="context">An <see cref="ISearchContext"/> object to use to search for the elements.</param>
        /// <returns>The first matching <see cref="IWebElement"/> on the current context.</returns>
        public static IWebElement FindElement(this By by, ISearchContext context)
            => by.FindElementAsync(context).ConfigureAwait(false).GetAwaiter().GetResult();

        /// <summary>
        /// Finds all elements matching the criteria.
        /// </summary>
        /// <param name="context">An <see cref="ISearchContext"/> object to use to search for the elements.</param>
        /// <returns>A <see cref="ReadOnlyCollection{T}"/> of all <see cref="IWebElement">WebElements</see>
        /// matching the current criteria, or an empty list if nothing matches.</returns>
        public static ReadOnlyCollection<IWebElement> FindElements(this By by, ISearchContext context)
            => by.FindElementsAsync(context).ConfigureAwait(false).GetAwaiter().GetResult();

        #endregion


        #region IDevTools

        /// <summary>
        /// Creates a session to communicate with a browser using a Developer Tools debugging protocol.
        /// </summary>
        /// <returns>The active session to use to communicate with the Developer Tools debugging protocol.</returns>
        public static DevToolsSession GetDevToolsSession(this IDevTools devTools)
            => devTools.GetDevToolsSessionAsync().ConfigureAwait(false).GetAwaiter().GetResult();

        /// <summary>
        /// Creates a session to communicate with a browser using a specific version of the Developer Tools debugging protocol.
        /// </summary>
        /// <param name="protocolVersion">The specific version of the Developer Tools debugging protocol to use.</param>
        /// <returns>The active session to use to communicate with the Developer Tools debugging protocol.</returns>
        public static DevToolsSession GetDevToolsSession(this IDevTools devTools, int protocolVersion)
            => devTools.GetDevToolsSessionAsync(protocolVersion).ConfigureAwait(false).GetAwaiter().GetResult();

        /// <summary>
        /// Closes a DevTools session
        /// </summary>
        public static void CloseDevToolsSession(this IDevTools devTools)
            => devTools.CloseDevToolsSessionAsync().ConfigureAwait(false).GetAwaiter().GetResult();

        #endregion IDevTools


        #region IFindsElement

        /// <summary>
        /// Finds the first element matching the specified value using the specified mechanism.
        /// </summary>
        /// <param name="mechanism">The mechanism to use when matching.</param>
        /// <param name="value">The value to match.</param>
        /// <returns>The first <see cref="IWebElement"/> matching the criteria.</returns>
        public static IWebElement FindElement(this IFindsElement findsElement, string mechanism, string value)
            => findsElement.FindElementAsync(mechanism, value).ConfigureAwait(false).GetAwaiter().GetResult();

        /// <summary>
        /// Finds all elements matching the specified value using the specified mechanism.
        /// </summary>
        /// <param name="mechanism">The mechanism to use when matching.</param>
        /// <param name="value">The value to match.</param>
        /// <returns><see cref="IWebElement">IWebElements</see> matching the criteria.</returns>
        public static ReadOnlyCollection<IWebElement> FindElements(this IFindsElement findsElement, string mechanism, string value)
            => findsElement.FindElementsAsync(mechanism, value).ConfigureAwait(false).GetAwaiter().GetResult();

        #endregion IFindsElement


        #region IHasVirtualAuthenticator

        /// <summary>
        /// Creates a Virtual Authenticator.
        /// </summary>
        /// <param name="options"> VirtualAuthenticator Options (https://w3c.github.io/webauthn/#sctn-automation-virtual-authenticators)</param>
        /// <returns> Authenticator id as string </returns>
        public static string AddVirtualAuthenticator(this IHasVirtualAuthenticator hasVirtualAuthenticator, VirtualAuthenticatorOptions options)
            => hasVirtualAuthenticator.AddVirtualAuthenticatorAsync(options).ConfigureAwait(false).GetAwaiter().GetResult();

        /// <summary>
        /// Removes the Virtual Authenticator
        /// </summary>
        /// <param name="authenticatorId"> Id as string that uniquely identifies a Virtual Authenticator</param>
        public static void RemoveVirtualAuthenticator(this IHasVirtualAuthenticator hasVirtualAuthenticator, string authenticatorId)
            => hasVirtualAuthenticator.RemoveVirtualAuthenticatorAsync(authenticatorId).ConfigureAwait(false).GetAwaiter().GetResult();

        /// <summary>
        /// Add a credential to the Virtual Authenticator
        /// </summary>
        /// <param name="credential"> The credential to be stored in the Virtual Authenticator</param>
        public static void AddCredential(this IHasVirtualAuthenticator hasVirtualAuthenticator, Credential credential)
            => hasVirtualAuthenticator.AddCredentialAsync(credential).ConfigureAwait(false).GetAwaiter().GetResult();

        /// <summary>
        /// Retrieves all the credentials stored in the Virtual Authenticator
        /// </summary>
        /// <returns> List of credentials </returns>
        public static List<Credential> GetCredentials(this IHasVirtualAuthenticator hasVirtualAuthenticator)
            => hasVirtualAuthenticator.GetCredentialsAsync().ConfigureAwait(false).GetAwaiter().GetResult();

        /// <summary>
        /// Removes the credential identified by the credentialId from the Virtual Authenticator.
        /// </summary>
        /// <param name="credentialId"> The id as byte array that uniquely identifies a credential </param>
        public static void RemoveCredential(this IHasVirtualAuthenticator hasVirtualAuthenticator, byte[] credentialId)
            => hasVirtualAuthenticator.RemoveCredentialAsync(credentialId).ConfigureAwait(false).GetAwaiter().GetResult();

        /// <summary>
        /// Removes the credential identified by the credentialId from the Virtual Authenticator.
        /// </summary>
        /// <param name="credentialId"> The id as byte array that uniquely identifies a credential </param>
        public static void RemoveCredential(this IHasVirtualAuthenticator hasVirtualAuthenticator, string credentialId)
            => hasVirtualAuthenticator.RemoveCredentialAsync(credentialId).ConfigureAwait(false).GetAwaiter().GetResult();

        /// <summary>
        /// Removes all the credentials stored in the Virtual Authenticator.
        /// </summary>
        public static void RemoveAllCredentials(this IHasVirtualAuthenticator hasVirtualAuthenticator)
            => hasVirtualAuthenticator.RemoveAllCredentialsAsync().ConfigureAwait(false).GetAwaiter().GetResult();

        /// <summary>
        ///  Sets the isUserVerified property for the Virtual Authenticator.
        /// </summary>
        /// <param name="verified">The boolean value representing value to be set </param>
        public static void SetUserVerified(this IHasVirtualAuthenticator hasVirtualAuthenticator, bool verified)
            => hasVirtualAuthenticator.SetUserVerifiedAsync(verified).ConfigureAwait(false).GetAwaiter().GetResult();

        #endregion IHasVirtualAuthenticator


        #region IActionExecutor

        /// <summary>
        /// Performs the specified list of actions with this action executor.
        /// </summary>
        /// <param name="actionSequenceList">The list of action sequences to perform.</param>
        public static void PerformActions(this IActionExecutor actionExecutor, IList<ActionSequence> actionSequenceList)
            => actionExecutor.PerformActionsAsync(actionSequenceList).ConfigureAwait(false).GetAwaiter().GetResult();

        /// <summary>
        /// Resets the input state of the action executor.
        /// </summary>
        public static void ResetInputState(this IActionExecutor actionExecutor)
             => actionExecutor.ResetInputStateAsync().ConfigureAwait(false).GetAwaiter().GetResult();

        #endregion IActionExecutor


        #region IAlert

        /// <summary>
        /// Dismisses the alert.
        /// </summary>
        public static void Dismiss(this IAlert alert)
            => alert.DismissAsync().ConfigureAwait(false).GetAwaiter().GetResult();

        /// <summary>
        /// Accepts the alert.
        /// </summary>
        public static void Accept(this IAlert alert)
            => alert.AcceptAsync().ConfigureAwait(false).GetAwaiter().GetResult();

        /// <summary>
        /// Sends keys to the alert.
        /// </summary>
        /// <param name="keysToSend">The keystrokes to send.</param>
        public static void SendKeys(this IAlert alert, string keysToSend)
            => alert.SendKeysAsync(keysToSend).ConfigureAwait(false).GetAwaiter().GetResult();

        #endregion IAlert


        #region ICookieJar

        /// <summary>
        /// Adds a cookie to the current page.
        /// </summary>
        /// <param name="cookie">The <see cref="Cookie"/> object to be added.</param>
        public static void AddCookie(this ICookieJar cookieJar, Cookie cookie)
            => cookieJar.AddCookieAsync(cookie).ConfigureAwait(false).GetAwaiter().GetResult();

        /// <summary>
        /// Gets a cookie with the specified name.
        /// </summary>
        /// <param name="name">The name of the cookie to retrieve.</param>
        /// <returns>The <see cref="Cookie"/> containing the name. Returns <see langword="null"/>
        /// if no cookie with the specified name is found.</returns>
        public static Cookie GetCookieNamed(this ICookieJar cookieJar, string name)
            => cookieJar.GetCookieNamedAsync(name).ConfigureAwait(false).GetAwaiter().GetResult();

        /// <summary>
        /// Deletes the specified cookie from the page.
        /// </summary>
        /// <param name="cookie">The <see cref="Cookie"/> to be deleted.</param>
        public static void DeleteCookie(this ICookieJar cookieJar, Cookie cookie)
            => cookieJar.DeleteCookieAsync(cookie).ConfigureAwait(false).GetAwaiter().GetResult();

        /// <summary>
        /// Deletes the cookie with the specified name from the page.
        /// </summary>
        /// <param name="name">The name of the cookie to be deleted.</param>
        public static void DeleteCookieNamed(this ICookieJar cookieJar, string name)
            => cookieJar.DeleteCookieNamedAsync(name).ConfigureAwait(false).GetAwaiter().GetResult();

        /// <summary>
        /// Deletes all cookies from the page.
        /// </summary>
        public static void DeleteAllCookies(this ICookieJar cookieJar)
            => cookieJar.DeleteAllCookiesAsync().ConfigureAwait(false).GetAwaiter().GetResult();

        #endregion ICookieJar


        #region ICustomDriverCommandExecutor

        /// <summary>
        /// Executes a command with this driver.
        /// </summary>
        /// <param name="driverCommandToExecute">The name of the command to execute. The command name must be registered with the command executor, and must not be a command name known to this driver type.</param>
        /// <param name="parameters">A <see cref="Dictionary{K, V}"/> containing the names and values of the parameters of the command.</param>
        /// <returns>An object that contains the value returned by the command, if any.</returns>
        public static object ExecuteCustomDriverCommand(this ICustomDriverCommandExecutor customDriverCommandExecutor, string driverCommandToExecute, Dictionary<string, object> parameters)
            => customDriverCommandExecutor.ExecuteCustomDriverCommandAsync(driverCommandToExecute, parameters).ConfigureAwait(false).GetAwaiter().GetResult();

        #endregion ICustomDriverCommandExecutor


        #region IJavaScriptExecutor

        /// <summary>
        /// Executes JavaScript in the context of the currently selected frame or window.
        /// </summary>
        /// <param name="script">The JavaScript code to execute.</param>
        /// <param name="args">The arguments to the script.</param>
        /// <returns>The value returned by the script.</returns>
        /// <remarks>
        /// <para>
        /// The <see cref="ExecuteScript"/>method executes JavaScript in the context of
        /// the currently selected frame or window. This means that "document" will refer
        /// to the current document. If the script has a return value, then the following
        /// steps will be taken:
        /// </para>
        /// <para>
        /// <list type="bullet">
        /// <item><description>For an HTML element, this method returns a <see cref="IWebElement"/></description></item>
        /// <item><description>For a number, a <see cref="long"/> is returned</description></item>
        /// <item><description>For a boolean, a <see cref="bool"/> is returned</description></item>
        /// <item><description>For all other cases a <see cref="string"/> is returned.</description></item>
        /// <item><description>For an array,we check the first element, and attempt to return a
        /// <see cref="List{T}"/> of that type, following the rules above. Nested lists are not
        /// supported.</description></item>
        /// <item><description>If the value is null or there is no return value,
        /// <see langword="null"/> is returned.</description></item>
        /// </list>
        /// </para>
        /// <para>
        /// Arguments must be a number (which will be converted to a <see cref="long"/>),
        /// a <see cref="bool"/>, a <see cref="string"/> or a <see cref="IWebElement"/>,
        /// or a <see cref="IWrapsElement"/>.
        /// An exception will be thrown if the arguments do not meet these criteria.
        /// The arguments will be made available to the JavaScript via the "arguments" magic
        /// variable, as if the function were called via "Function.apply"
        /// </para>
        /// </remarks>
        public static object ExecuteScript(this IJavaScriptExecutor javaScriptExecutor, string script, params object[] args)
            => javaScriptExecutor.ExecuteScriptAsync(script, args).ConfigureAwait(false).GetAwaiter().GetResult();

        /// <summary>
        /// Executes JavaScript in the context of the currently selected frame or window.
        /// </summary>
        /// <param name="script">A <see cref="PinnedScript"/> object containing the code to execute.</param>
        /// <param name="args">The arguments to the script.</param>
        /// <returns>The value returned by the script.</returns>
        /// <remarks>
        /// <para>
        /// The <see cref="ExecuteScript"/>method executes JavaScript in the context of
        /// the currently selected frame or window. This means that "document" will refer
        /// to the current document. If the script has a return value, then the following
        /// steps will be taken:
        /// </para>
        /// <para>
        /// <list type="bullet">
        /// <item><description>For an HTML element, this method returns a <see cref="IWebElement"/></description></item>
        /// <item><description>For a number, a <see cref="long"/> is returned</description></item>
        /// <item><description>For a boolean, a <see cref="bool"/> is returned</description></item>
        /// <item><description>For all other cases a <see cref="string"/> is returned.</description></item>
        /// <item><description>For an array,we check the first element, and attempt to return a
        /// <see cref="List{T}"/> of that type, following the rules above. Nested lists are not
        /// supported.</description></item>
        /// <item><description>If the value is null or there is no return value,
        /// <see langword="null"/> is returned.</description></item>
        /// </list>
        /// </para>
        /// <para>
        /// Arguments must be a number (which will be converted to a <see cref="long"/>),
        /// a <see cref="bool"/>, a <see cref="string"/> or a <see cref="IWebElement"/>,
        /// or a <see cref="IWrapsElement"/>.
        /// An exception will be thrown if the arguments do not meet these criteria.
        /// The arguments will be made available to the JavaScript via the "arguments" magic
        /// variable, as if the function were called via "Function.apply"
        /// </para>
        /// </remarks>
        public static object ExecuteScript(this IJavaScriptExecutor javaScriptExecutor, PinnedScript script, params object[] args)
             => javaScriptExecutor.ExecuteScriptAsync(script, args).ConfigureAwait(false).GetAwaiter().GetResult();

        /// <summary>
        /// Executes JavaScript asynchronously in the context of the currently selected frame or window.
        /// </summary>
        /// <param name="script">The JavaScript code to execute.</param>
        /// <param name="args">The arguments to the script.</param>
        /// <returns>The value returned by the script.</returns>
        public static object ExecuteAsyncScript(this IJavaScriptExecutor javaScriptExecutor, string script, params object[] args)
             => javaScriptExecutor.ExecuteAsyncScriptAsync(script, args).ConfigureAwait(false).GetAwaiter().GetResult();

        #endregion IJavaScriptExecutor

        #region ILogs

        /// <summary>
        /// Gets the set of <see cref="LogEntry"/> objects for a specified log.
        /// </summary>
        /// <param name="logKind">The log for which to retrieve the log entries.
        /// Log types can be found in the <see cref="LogType"/> class.</param>
        /// <returns>The list of <see cref="LogEntry"/> objects for the specified log.</returns>
        public static ReadOnlyCollection<LogEntry> GetLog(this ILogs logs, string logKind)
            => logs.GetLogAsync(logKind).ConfigureAwait(false).GetAwaiter().GetResult();

        #endregion ILogs

        #region INavigation

        /// <summary>
        /// Move back a single entry in the browser's history.
        /// </summary>
        public static void Back(this INavigation navigation)
            => navigation.BackAsync().ConfigureAwait(false).GetAwaiter().GetResult();

        /// <summary>
        /// Move a single "item" forward in the browser's history.
        /// </summary>
        /// <remarks>Does nothing if we are on the latest page viewed.</remarks>
        public static void Forward(this INavigation navigation)
            => navigation.ForwardAsync().ConfigureAwait(false).GetAwaiter().GetResult();

        /// <summary>
        ///  Load a new web page in the current browser window.
        /// </summary>
        /// <param name="url">The URL to load. It is best to use a fully qualified URL</param>
        /// <remarks>
        /// Calling the <see cref="GoToUrl(string)"/> method will load a new web page in the current browser window.
        /// This is done using an HTTP GET operation, and the method will block until the
        /// load is complete. This will follow redirects issued either by the server or
        /// as a meta-redirect from within the returned HTML. Should a meta-redirect "rest"
        /// for any duration of time, it is best to wait until this timeout is over, since
        /// should the underlying page change while your test is executing the results of
        /// future calls against this interface will be against the freshly loaded page.
        /// </remarks>
        public static void GoToUrl(this INavigation navigation, string url)
            => navigation.GoToUrlAsync(url).ConfigureAwait(false).GetAwaiter().GetResult();

        /// <summary>
        ///  Load a new web page in the current browser window.
        /// </summary>
        /// <param name="url">The URL to load.</param>
        /// <remarks>
        /// Calling the <see cref="GoToUrl(System.Uri)"/> method will load a new web page in the current browser window.
        /// This is done using an HTTP GET operation, and the method will block until the
        /// load is complete. This will follow redirects issued either by the server or
        /// as a meta-redirect from within the returned HTML. Should a meta-redirect "rest"
        /// for any duration of time, it is best to wait until this timeout is over, since
        /// should the underlying page change while your test is executing the results of
        /// future calls against this interface will be against the freshly loaded page.
        /// </remarks>
        public static void GoToUrl(this INavigation navigation, Uri url)
            => navigation.GoToUrlAsync(url).ConfigureAwait(false).GetAwaiter().GetResult();

        /// <summary>
        /// Refreshes the current page.
        /// </summary>
        public static void Refresh(this INavigation navigation)
            => navigation.RefreshAsync().ConfigureAwait(false).GetAwaiter().GetResult();

        #endregion INavigation


        #region ISearchContext

        /// <summary>
        /// Finds the first <see cref="IWebElement"/> using the given method.
        /// </summary>
        /// <param name="by">The locating mechanism to use.</param>
        /// <returns>The first matching <see cref="IWebElement"/> on the current context.</returns>
        /// <exception cref="NoSuchElementException">If no element matches the criteria.</exception>
        public static IWebElement FindElement(this ISearchContext searchContext, By by)
            => searchContext.FindElementAsync(by).ConfigureAwait(false).GetAwaiter().GetResult();

        /// <summary>
        /// Finds all <see cref="IWebElement">IWebElements</see> within the current context
        /// using the given mechanism.
        /// </summary>
        /// <param name="by">The locating mechanism to use.</param>
        /// <returns>A <see cref="ReadOnlyCollection{T}"/> of all <see cref="IWebElement">WebElements</see>
        /// matching the current criteria, or an empty list if nothing matches.</returns>
        public static ReadOnlyCollection<IWebElement> FindElements(this ISearchContext searchContext, By by)
            => searchContext.FindElementsAsync(by).ConfigureAwait(false).GetAwaiter().GetResult();

        #endregion ISearchContext


        #region ISupportsPrint

        /// <summary>
        /// Gets a <see cref="PrintDocument"/> object representing a PDF-formatted print representation of the page.
        /// </summary>
        /// <param name="printOptions">A <see cref="PrintOptions"/> object describing the options of the printed document.</param>
        /// <returns>The <see cref="PrintDocument"/> object containing the PDF-formatted print representation of the page.</returns>
        public static PrintDocument Print(this ISupportsPrint supportsPrint, PrintOptions printOptions)
            => supportsPrint.PrintAsync(printOptions).ConfigureAwait(false).GetAwaiter().GetResult();

        #endregion ISupportsPrint


        #region ITakesScreenshot

        /// <summary>
        /// Gets a <see cref="Screenshot"/> object representing the image of the page on the screen.
        /// </summary>
        /// <returns>A <see cref="Screenshot"/> object containing the image.</returns>
        public static Screenshot GetScreenshot(this ITakesScreenshot takesScreenshot)
            => takesScreenshot.GetScreenshotAsync().ConfigureAwait(false).GetAwaiter().GetResult();

        #endregion ITakesScreenshot


        #region ITargetLocator

        /// <summary>
        /// Select a frame by its (zero-based) index.
        /// </summary>
        /// <param name="frameIndex">The zero-based index of the frame to select.</param>
        /// <returns>An <see cref="IWebDriver"/> instance focused on the specified frame.</returns>
        /// <exception cref="NoSuchFrameException">If the frame cannot be found.</exception>
        public static IWebDriver Frame(this ITargetLocator targetLocator, int frameIndex)
            => targetLocator.FrameAsync(frameIndex).ConfigureAwait(false).GetAwaiter().GetResult();

        /// <summary>
        /// Select a frame by its name or ID.
        /// </summary>
        /// <param name="frameName">The name of the frame to select.</param>
        /// <returns>An <see cref="IWebDriver"/> instance focused on the specified frame.</returns>
        /// <exception cref="NoSuchFrameException">If the frame cannot be found.</exception>
        public static IWebDriver Frame(this ITargetLocator targetLocator, string frameName)
            => targetLocator.FrameAsync(frameName).ConfigureAwait(false).GetAwaiter().GetResult();

        /// <summary>
        /// Select a frame using its previously located <see cref="IWebElement"/>
        /// </summary>
        /// <param name="frameElement">The frame element to switch to.</param>
        /// <returns>An <see cref="IWebDriver"/> instance focused on the specified frame.</returns>
        /// <exception cref="NoSuchFrameException">If the element is neither a FRAME nor an IFRAME element.</exception>
        /// <exception cref="StaleElementReferenceException">If the element is no longer valid.</exception>
        public static IWebDriver Frame(this ITargetLocator targetLocator, IWebElement frameElement)
            => targetLocator.FrameAsync(frameElement).ConfigureAwait(false).GetAwaiter().GetResult();

        /// <summary>
        /// Select the parent frame of the currently selected frame.
        /// </summary>
        /// <returns>An <see cref="IWebDriver"/> instance focused on the specified frame.</returns>
        public static IWebDriver ParentFrame(this ITargetLocator targetLocator)
            => targetLocator.ParentFrameAsync().ConfigureAwait(false).GetAwaiter().GetResult();

        /// <summary>
        /// Switches the focus of future commands for this driver to the window with the given name.
        /// </summary>
        /// <param name="windowHandleOrName">The name of the window to select.</param>
        /// <returns>An <see cref="IWebDriver"/> instance focused on the given window.</returns>
        /// <exception cref="NoSuchWindowException">If the window cannot be found.</exception>
        public static IWebDriver Window(this ITargetLocator targetLocator, string windowHandleOrName)
            => targetLocator.WindowAsync(windowHandleOrName).ConfigureAwait(false).GetAwaiter().GetResult();

        /// <summary>
        /// Creates a new browser window and switches the focus for future commands
        /// of this driver to the new window.
        /// </summary>
        /// <param name="typeHint">The type of new browser window to be created.
        /// The created window is not guaranteed to be of the requested type; if
        /// the driver does not support the requested type, a new browser window
        /// will be created of whatever type the driver does support.</param>
        /// <returns>An <see cref="IWebDriver"/> instance focused on the new browser.</returns>
        public static IWebDriver NewWindow(this ITargetLocator targetLocator, WindowType typeHint)
            => targetLocator.NewWindowAsync(typeHint).ConfigureAwait(false).GetAwaiter().GetResult();

        /// <summary>
        /// Selects either the first frame on the page or the main document when a page contains iFrames.
        /// </summary>
        /// <returns>An <see cref="IWebDriver"/> instance focused on the default frame.</returns>
        public static IWebDriver DefaultContent(this ITargetLocator targetLocator)
            => targetLocator.DefaultContentAsync().ConfigureAwait(false).GetAwaiter().GetResult();

        /// <summary>
        /// Switches to the element that currently has the focus, or the body element
        /// if no element with focus can be detected.
        /// </summary>
        /// <returns>An <see cref="IWebElement"/> instance representing the element
        /// with the focus, or the body element if no element with focus can be detected.</returns>
        public static IWebElement ActiveElement(this ITargetLocator targetLocator)
            => targetLocator.ActiveElementAsync().ConfigureAwait(false).GetAwaiter().GetResult();

        /// <summary>
        /// Switches to the currently active modal dialog for this particular driver instance.
        /// </summary>
        /// <returns>A handle to the dialog.</returns>
        public static IAlert Alert(this ITargetLocator targetLocator)
            => targetLocator.AlertAsync().ConfigureAwait(false).GetAwaiter().GetResult();

        #endregion ITargetLocator


        #region IWebDriver

        /// <summary>
        /// Close the current window, quitting the browser if it is the last window currently open.
        /// </summary>
        public static void Close(this IWebDriver webDriver)
            => webDriver.CloseAsync().ConfigureAwait(false).GetAwaiter().GetResult();

        #endregion IWebDriver


        #region IWebElement

        /// <summary>
        /// Clears the content of this element.
        /// </summary>
        /// <remarks>If this element is a text entry element, the <see cref="Clear"/>
        /// method will clear the value. It has no effect on other elements. Text entry elements
        /// are defined as elements with INPUT or TEXTAREA tags.</remarks>
        /// <exception cref="StaleElementReferenceException">Thrown when the target element is no longer valid in the document DOM.</exception>
        public static void Clear(this IWebElement webElement)
            => webElement.ClearAsync().ConfigureAwait(false).GetAwaiter().GetResult();

        /// <summary>
        /// Simulates typing text into the element.
        /// </summary>
        /// <param name="text">The text to type into the element.</param>
        /// <remarks>The text to be typed may include special characters like arrow keys,
        /// backspaces, function keys, and so on. Valid special keys are defined in
        /// <see cref="Keys"/>.</remarks>
        /// <seealso cref="Keys"/>
        /// <exception cref="InvalidElementStateException">Thrown when the target element is not enabled.</exception>
        /// <exception cref="ElementNotVisibleException">Thrown when the target element is not visible.</exception>
        /// <exception cref="StaleElementReferenceException">Thrown when the target element is no longer valid in the document DOM.</exception>
        public static void SendKeys(this IWebElement webElement, string text)
            => webElement.SendKeysAsync(text).ConfigureAwait(false).GetAwaiter().GetResult();

        /// <summary>
        /// Submits this element to the web server.
        /// </summary>
        /// <remarks>If this current element is a form, or an element within a form,
        /// then this will be submitted to the web server. If this causes the current
        /// page to change, then this method will block until the new page is loaded.</remarks>
        /// <exception cref="StaleElementReferenceException">Thrown when the target element is no longer valid in the document DOM.</exception>
        public static void Submit(this IWebElement webElement)
            => webElement.SubmitAsync().ConfigureAwait(false).GetAwaiter().GetResult();

        /// <summary>
        /// Clicks this element.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Click this element. If the click causes a new page to load, the <see cref="Click"/>
        /// method will attempt to block until the page has loaded. After calling the
        /// <see cref="Click"/> method, you should discard all references to this
        /// element unless you know that the element and the page will still be present.
        /// Otherwise, any further operations performed on this element will have an undefined.
        /// behavior.
        /// </para>
        /// <para>
        /// If this element is not clickable, then this operation is ignored. This allows you to
        /// simulate a users to accidentally missing the target when clicking.
        /// </para>
        /// </remarks>
        /// <exception cref="ElementNotVisibleException">Thrown when the target element is not visible.</exception>
        /// <exception cref="StaleElementReferenceException">Thrown when the target element is no longer valid in the document DOM.</exception>
        public static void Click(this IWebElement webElement)
            => webElement.ClickAsync().ConfigureAwait(false).GetAwaiter().GetResult();

        /// <summary>
        /// Gets the value of the specified attribute for this element.
        /// </summary>
        /// <param name="attributeName">The name of the attribute.</param>
        /// <returns>The attribute's current value. Returns a <see langword="null"/> if the
        /// value is not set.</returns>
        /// <remarks>The <see cref="GetAttribute"/> method will return the current value
        /// of the attribute, even if the value has been modified after the page has been
        /// loaded. Note that the value of the following attributes will be returned even if
        /// there is no explicit attribute on the element:
        /// <list type="table">
        /// <listheader>
        /// <term>Attribute name</term>
        /// <term>Value returned if not explicitly specified</term>
        /// <term>Valid element types</term>
        /// </listheader>
        /// <item>
        /// <description>checked</description>
        /// <description>checked</description>
        /// <description>Check Box</description>
        /// </item>
        /// <item>
        /// <description>selected</description>
        /// <description>selected</description>
        /// <description>Options in Select elements</description>
        /// </item>
        /// <item>
        /// <description>disabled</description>
        /// <description>disabled</description>
        /// <description>Input and other UI elements</description>
        /// </item>
        /// </list>
        /// </remarks>
        /// <exception cref="StaleElementReferenceException">Thrown when the target element is no longer valid in the document DOM.</exception>
        public static string GetAttribute(this IWebElement webElement, string attributeName)
            => webElement.GetAttributeAsync(attributeName).ConfigureAwait(false).GetAwaiter().GetResult();

        /// <summary>
        /// Gets the value of a declared HTML attribute of this element.
        /// </summary>
        /// <param name="attributeName">The name of the HTML attribute to get the value of.</param>
        /// <returns>The HTML attribute's current value. Returns a <see langword="null"/> if the
        /// value is not set or the declared attribute does not exist.</returns>
        /// <exception cref="StaleElementReferenceException">Thrown when the target element is no longer valid in the document DOM.</exception>
        /// <remarks>
        /// As opposed to the <see cref="GetAttribute(string)"/> method, this method
        /// only returns attributes declared in the element's HTML markup. To access the value
        /// of an IDL property of the element, either use the <see cref="GetAttribute(string)"/>
        /// method or the <see cref="GetDomProperty(string)"/> method.
        /// </remarks>
        public static string GetDomAttribute(this IWebElement webElement, string attributeName)
            => webElement.GetDomAttributeAsync(attributeName).ConfigureAwait(false).GetAwaiter().GetResult();

        /// <summary>
        /// Gets the value of a JavaScript property of this element.
        /// </summary>
        /// <param name="propertyName">The name of the JavaScript property to get the value of.</param>
        /// <returns>The JavaScript property's current value. Returns a <see langword="null"/> if the
        /// value is not set or the property does not exist.</returns>
        /// <exception cref="StaleElementReferenceException">Thrown when the target element is no longer valid in the document DOM.</exception>
        public static string GetDomProperty(this IWebElement webElement, string propertyName)
            => webElement.GetDomPropertyAsync(propertyName).ConfigureAwait(false).GetAwaiter().GetResult();

        /// <summary>
        /// Gets the value of a CSS property of this element.
        /// </summary>
        /// <param name="propertyName">The name of the CSS property to get the value of.</param>
        /// <returns>The value of the specified CSS property.</returns>
        /// <remarks>The value returned by the <see cref="GetCssValue"/>
        /// method is likely to be unpredictable in a cross-browser environment.
        /// Color values should be returned as hex strings. For example, a
        /// "background-color" property set as "green" in the HTML source, will
        /// return "#008000" for its value.</remarks>
        /// <exception cref="StaleElementReferenceException">Thrown when the target element is no longer valid in the document DOM.</exception>
        public static string GetCssValue(this IWebElement webElement, string propertyName)
            => webElement.GetCssValueAsync(propertyName).ConfigureAwait(false).GetAwaiter().GetResult();

        /// <summary>
        /// Gets the representation of an element's shadow root for accessing the shadow DOM of a web component.
        /// </summary>
        /// <exception cref="NoSuchShadowRootException">Thrown when this element does not have a shadow root.</exception>
        /// <returns>A shadow root representation.</returns>
        public static ISearchContext GetShadowRoot(this IWebElement webElement)
            => webElement.GetShadowRootAsync().ConfigureAwait(false).GetAwaiter().GetResult();

        #endregion IWebElement


        #region IWindow

        /// <summary>
        /// Maximizes the current window if it is not already maximized.
        /// </summary>
        public static void Maximize(this IWindow window)
            => window.MaximizeAsync().ConfigureAwait(false).GetAwaiter().GetResult();

        /// <summary>
        /// Minimizes the current window if it is not already minimized.
        /// </summary>
        public static void Minimize(this IWindow window)
            => window.MinimizeAsync().ConfigureAwait(false).GetAwaiter().GetResult();

        /// <summary>
        /// Sets the current window to full screen if it is not already in that state.
        /// </summary>
        public static void FullScreen(this IWindow window)
            => window.FullScreenAsync().ConfigureAwait(false).GetAwaiter().GetResult();

        #endregion IWindow
    }
}
