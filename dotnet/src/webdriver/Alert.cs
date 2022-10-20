// <copyright file="Alert.cs" company="WebDriver Committers">
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
using System.Threading.Tasks;

namespace OpenQA.Selenium
{
    /// <summary>
    /// Defines the interface through which the user can manipulate JavaScript alerts.
    /// </summary>
    internal class Alert : IAlert
    {
        private WebDriver driver;

        /// <summary>
        /// Initializes a new instance of the <see cref="Alert"/> class.
        /// </summary>
        /// <param name="driver">The <see cref="WebDriver"/> for which the alerts will be managed.</param>
        public Alert(WebDriver driver)
        {
            this.driver = driver;
        }

        /// <summary>
        /// Gets the text of the alert.
        /// </summary>
        public string Text
        {
            get
            {
                Response commandResponse = this.driver.InternalExecuteAsync(DriverCommand.GetAlertText, null).ConfigureAwait(false).GetAwaiter().GetResult();
                return commandResponse.Value.ToString();
            }
        }

        /// <summary>
        /// Dismisses the alert.
        /// </summary>
        public Task DismissAsync()
        {
            return this.driver.InternalExecuteAsync(DriverCommand.DismissAlert, null);
        }

        /// <summary>
        /// Accepts the alert.
        /// </summary>
        public Task AcceptAsync()
        {
            return this.driver.InternalExecuteAsync(DriverCommand.AcceptAlert, null);
        }

        /// <summary>
        /// Sends keys to the alert.
        /// </summary>
        /// <param name="keysToSend">The keystrokes to send.</param>
        public Task SendKeysAsync(string keysToSend)
        {
            if (keysToSend == null)
            {
                throw new ArgumentNullException(nameof(keysToSend), "Keys to send must not be null.");
            }

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("text", keysToSend);

            return this.driver.InternalExecuteAsync(DriverCommand.SetAlertValue, parameters);
        }
    }
}
