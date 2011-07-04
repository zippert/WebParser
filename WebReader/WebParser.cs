

namespace WebReader
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Windows;

    class WebParser
    {
        /// <summary>
        /// Gets or sets Url.
        /// </summary>
        private string Url { get; set; }

        /// <summary>
        /// Gets or sets Enc.
        /// </summary>
        private Encoding Enc { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="WebParser"/> class.
        /// </summary>
        /// <param name="url">
        /// The url to parse.
        /// </param>
        /// <param name="encoding">
        /// The encoding of the input URL
        /// </param>
        public WebParser(string url, Encoding encoding = null)
        {
            this.Url = url;
            if (encoding == null)
            {
                this.Enc = Encoding.UTF8;
            }
            else
            {
                this.Enc = encoding;    
            }
            
        }

        /// <summary>
        /// Parses website and returns the result
        /// </summary>
        /// <returns>
        /// The webresponse as a string
        /// </returns>
        public string Parse()
        {
            WebResponse response = null;
            StreamReader reader = null;
            string result = null;
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(this.Url);
                request.Method = "GET";
                response = request.GetResponse();
                var respStream = response.GetResponseStream();
                if (respStream != null)
                {
                    reader = new StreamReader(respStream, this.Enc);
                    result = reader.ReadToEnd();
                }
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }

                if (response != null)
                {
                    response.Close();
                }
            }

            return result;
        }

    }
}
