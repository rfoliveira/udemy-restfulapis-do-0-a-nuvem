// Ref.: https://auth0.com/blog/how-to-create-a-vue-plugin/

/**
    The main goal of a Vue plugin is to expose functionality at a global level in your Vue application. 
    While there is no strictly defined scope for Vue plugins, these are their most common use cases:

    - Add some global methods or properties.
    - Add one or more global assets, such as directives or filters.
    - Add component options using a global mixin.
    - Add methods to a Vue instance by attaching them to the Vue.prototype.
 */

/**
 * External modules
 */
import Vue from 'vue';
import createAuth0Client from "@auth0/auth0-spa-js";

/**
 * Vue instance definition
 */
let instance;
export const getInstance = () => instance;

/**
 * Vue instance initialization
 * When you create a Vue instance, Vue adds all the properties found in the data property to its reactivity system. 
 * Vue mixes all the properties found in the methods property into the Vue instance. 
 * All the mixed methods have their this context bound to the created Vue instance automatically.
 * 
 * The data properties will hold the state of your authentication plugin. Those state properties should answer the following questions:
 * 
 * - Is there an active Auth0 client?
 * - Has the Auth0 SPA SDK loaded?
 * - Is the user authenticated?
 * - What is the profile information of the user?
 * - Has an error occurred during the authentication process?
 */
export const useAuth0 = ({
    onRedirectCallback = () => window.history.replaceState({}, document.title, window.location.pathname),
    redirectUri = window.location.origin,
    ...pluginOptions
}) => {
    if (instance) return instance;

    instance = new Vue({
        data() {
            return {
                // "this.auth0Client" will hold an instance of the Auth0 SPA SDK and give you access to all of its time-saving methods.
                auth0Client: null,
                isLoading: true,
                isAuthenticated: false,
                user: {},
                error: null
            }
        },
        methods: {
            // Method to handle the redirect from Auth0 back to your Vue application and to consume the results of the user authentication process.
            async handleRedirectCallback() {
                this.isLoading = true;

                try {

                    await this.auth0Client.handleRedirectCallback();
                    this.user = await this.auth0Client.getUser();
                    
                    this.isAuthenticated = true;

                } catch (error) {
                    this.error = error;
                } finally {
                    this.isLoading = false;
                }
            },

            // Method to redirect your users to Auth0 for logging in.
            loginWithRedirect(options) {
                return this.auth0Client.loginWithRedirect(options);
            },

            // Method to log users out and remove their session on the authorization server.
            logout(options) {
                return this.auth0Client.logout(options);
            },

            /*
                Your Vue application can receive an access token after a user successfully authenticates and authorizes access. 
                It passes the access token as a credential when it calls the target API. 
                The passed token informs the API that the bearer of the token has been authorized to access the API and perform specific actions on behalf of a user.

                Thus, with security in mind, you need a method that returns the access token. 
                Additionally, if the token is invalid or missing, the method should get a new one. 
                Usually, getting new access tokens requires the user to log in again. 
                However, The Auth0 SPA SDK lets you get one in the background without interrupting the user.
            */
            getTokenSilently(options) {
                return this.auth0Client.getTokenSilently(options);
            }
        },

        /** Use this lifecycle method to instantiate the SDK client */
        async created() {
            this.auth0Client = await createAuth0Client({
                ...pluginOptions,
                domain: pluginOptions.domain,
                client_id: pluginOptions.client_id,
                audience: pluginOptions.audience,
                redirect_uri: redirectUri
            });

            try {
                if (window.location.search.includes('code=') 
                    && window.location.search.includes('state='))
                {
                    const { appState } = await this.auth0Client.handleRedirectCallback();
                    onRedirectCallback(appState);
                }
            } catch (error) {
                this.error = error;
            } finally {
                this.isAuthenticated = await this.auth0Client.isAuthenticated();
                this.user = await this.auth0Client.getUser();
                this.isLoading = false;
            }
        }
    });

    return instance;
}

/**
 * Vue plugin definition
 */
/*
    A Vue plugin must expose an install() method, which Vue calls with the Vue constructor and some options as arguments. 
    You pass those options to useAuth0(), which map to its pluginOptions parameter. 
    You then store the instance that useAuth0() returns as a property of the Vue.prototype object, $auth. 
    The $auth property is now a global Vue property.

    Once you install this plugin in your application, you can access the user authentication functionality through the this context of any Vue component.
*/
export const Auth0Plugin = {
    install(Vue, options) {
        Vue.prototype.$auth = useAuth0(options);
    }
}