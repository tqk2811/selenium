// <copyright file="DesiredCapabilities.cs" company="WebDriver Committers">
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
using System.Threading.Tasks;
using System.Collections.Generic;

namespace OpenQA.Selenium.VirtualAuth
{
    public interface IHasVirtualAuthenticator
    {
        Task<string> AddVirtualAuthenticatorAsync(VirtualAuthenticatorOptions options);

        Task RemoveVirtualAuthenticatorAsync(string id);

        Task AddCredentialAsync(Credential credential);

        Task<List<Credential>> GetCredentialsAsync();

        Task RemoveCredentialAsync(byte[] credentialId);

        Task RemoveCredentialAsync(string credentialId);

        Task RemoveAllCredentialsAsync();

        Task SetUserVerifiedAsync(bool verified);
    }

    public static class IHasVirtualAuthenticatorExtensions
    {
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
    }
}
