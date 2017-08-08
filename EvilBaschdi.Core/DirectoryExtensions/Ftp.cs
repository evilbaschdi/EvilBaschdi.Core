using System;
using System.IO;
using System.Net;
using System.Text;

namespace EvilBaschdi.Core.DirectoryExtensions
{
    /// <summary>
    ///     Class for Ftp.
    /// </summary>
    public class Ftp
    {
        private readonly string _host;
        private readonly string _user;
        private readonly string _pass;
        private FtpWebRequest _ftpWebRequest;
        private FtpWebResponse _ftpWebResponse;
        private Stream _ftpStream;

        /// <exception cref="ArgumentNullException">
        ///     <paramref name="host" /> is <see langword="null" />.
        ///     <paramref name="user" /> is <see langword="null" />.
        ///     <paramref name="password" /> is <see langword="null" />.
        /// </exception>
        public Ftp(string host, string user, string password)
        {
            _host = host ?? throw new ArgumentNullException(nameof(host));
            _user = user ?? throw new ArgumentNullException(nameof(user));
            _pass = password ?? throw new ArgumentNullException(nameof(password));
        }

        /// <summary>
        ///     Returns a list of directories by given directory.
        /// </summary>
        /// <param name="directory"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"><paramref name="directory" /> is <see langword="null" />.</exception>
        /// <exception cref="InvalidOperationException">Condition.</exception>
        public string[] DirectoryListSimple(string directory)
        {
            if (directory == null)
            {
                throw new ArgumentNullException(nameof(directory));
            }
            try
            {
                _ftpWebRequest = (FtpWebRequest) WebRequest.Create($"{_host}/{directory}");
                _ftpWebRequest.Credentials = new NetworkCredential(_user, _pass);
                _ftpWebRequest.UseBinary = true;
                _ftpWebRequest.UsePassive = true;
                _ftpWebRequest.KeepAlive = true;
                _ftpWebRequest.Method = WebRequestMethods.Ftp.ListDirectory;
                _ftpWebResponse = (FtpWebResponse) _ftpWebRequest.GetResponse();
                _ftpStream = _ftpWebResponse.GetResponseStream();
                if (_ftpStream != null)
                {
                    var ftpReader = new StreamReader(_ftpStream);
                    var directoryRaw = new StringBuilder();
                    try
                    {
                        while (ftpReader.Peek() != -1)
                        {
                            directoryRaw.Append($"{ftpReader.ReadLine()}|");
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new InvalidOperationException(ex.Message, ex);
                    }
                    ftpReader.Close();
                    _ftpStream.Close();
                    _ftpWebResponse.Close();
                    _ftpWebRequest = null;
                    try
                    {
                        var directoryRawString = directoryRaw.ToString();
                        if (!string.IsNullOrWhiteSpace(directoryRawString))
                        {
                            var directoryList = directoryRawString.Split("|".ToCharArray());
                            return directoryList;
                        }
                    }

                    catch (Exception ex)
                    {
                        throw new InvalidOperationException(ex.Message, ex);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message, ex);
            }
            return new[] { "" };
        }
    }
}