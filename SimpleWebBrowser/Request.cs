using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace SimpleWebBrowser
{
    /// <summary>
    /// We don't need to instantiate this class as all its methods are defined with static
    /// </summary>
    abstract class Request
    {
        /// <summary>
        /// Sends a GET request to a website
        /// <param name="url">The url of the targeted website</param>
        /// <param name="Current">The tab 'from which' we send the request</param>
        /// <remarks>
        /// This method is marked with <c>async Task</c> to have an asynchronous method <c>GetResponseAsync()</c>
        /// this way the UI isn't blocked by the GET and its callback
        /// </remarks>
        /// </summary>
        public static async Task getRequest(string url, Tab CurrentTab)
        {
            //To be sure url is not empty
            if (!url.Equals(""))
            {
                //We try to send a GET request to the website
                try
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                    request.AutomaticDecompression = DecompressionMethods.GZip;

                    //Here is the asynchronous method, we wait for its callback with the await keyword
                    using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
                    using (Stream stream = response.GetResponseStream())
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        CurrentTab.htmlDisplay.Text = reader.ReadToEnd();
                    }
                }
                //In case we don't have a responseStatusCode we want to handle the exceptions/errors listed below
                catch (WebException ex) when ((ex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.NotFound)
                {
                    CurrentTab.htmlDisplay.Text = Properties.Resources.Error404;
                }
                catch (WebException ex) when ((ex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.Unauthorized)
                {
                    CurrentTab.htmlDisplay.Text = Properties.Resources.Error401;
                }
                catch (WebException ex) when ((ex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.Forbidden)
                {
                    CurrentTab.htmlDisplay.Text = Properties.Resources.Error403;
                }
                //For others undefined http errors we consider them as 400 errors, bad request
                catch (WebException ex)
                {
                    CurrentTab.htmlDisplay.Text = Properties.Resources.Error400 + " : " + ex.Message;
                }
                //Whatever appened, we need to enable and display the result in the appropriate control
                finally
                {
                    CurrentTab.htmlDisplay.Enabled = true;
                    CurrentTab.htmlDisplay.Show();
                }
            }
        }

        /// <summary>
        /// Sends a GET request to a website
        /// <param name="history">The global history of the browser</param>
        /// <param name="CurrentTab">The tab 'from which' we send the request</param>
        /// <remarks>
        /// This method is marked with <c>async</c> to have an asynchronous method <c>getRequest()</c>
        /// The purpose of the function is to add the url to the history and make sure we have a valid URI
        /// </remarks>
        /// </summary>
        static public async void CreateRequest(HistoryEntity.History history, Tab CurrentTab)
        {
            string hostName = null;
            //We formate the url in such a way that we have this scheme: "http://www.website.com"
            //The method .ToLower() isn't necessary but we keep in, just in case
            string url = Request.formatUrl(CurrentTab.urlEntry.Text).ToLower();
            try
            {
                Uri uri = new Uri(url);
                hostName = uri.Host;
                //We execute the method to get the HTML and display it
                await Request.getRequest(url, CurrentTab);
            }
            //In case we can't aim the server we catch the exception
            catch(UriFormatException ex)
            {
                CurrentTab.htmlDisplay.Text = Properties.Resources.ErrorDNS;
                hostName = url;
            }
            //Whatever happened we display the result and add the website to the logs
            finally
            {
                history.Add(hostName, url);
                CurrentTab.htmlDisplay.Enabled = true;
                CurrentTab.htmlDisplay.Show();
            }
        }

        /// <summary>
        /// Formats a string into a proper url
        /// <param name="str">A string that may not be a correct url</param>
        /// <remarks>
        /// We just add the missing parts of a correct url scheme.
        /// </remarks>
        /// </summary>
        private static string formatUrl(string str)
        {
            string finalUrl = str;
            // str is like "website.com" or "www.website.com"
            if (!(str.StartsWith("http://") || str.StartsWith("https://")))
            {
                //website is like "www.website.com"
                if (!str.StartsWith("www."))
                {
                    finalUrl = str.Insert(0, "www.");
                }
                finalUrl = finalUrl.Insert(0, "http://");
            }
            //We have a url with this scheme whichever the case we had
            return finalUrl;
        }                               
    }
}
