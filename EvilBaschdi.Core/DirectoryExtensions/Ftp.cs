using System;
using System.IO;
using System.Net;
using System.Text;
using System.Windows;

namespace EvilBaschdi.Core.DirectoryExtensions
{
    public class Ftp
    {
        private readonly string _host;
        private readonly string _user;
        private readonly string _pass;
        private FtpWebRequest _ftpWebRequest;
        private FtpWebResponse _ftpWebResponse;
        private Stream _ftpStream;

        public Ftp(string host, string user, string password)
        {
            if (host == null)
            {
                throw new ArgumentNullException(nameof(host));
            }
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (password == null)
            {
                throw new ArgumentNullException(nameof(password));
            }
            _host = host;
            _user = user;
            _pass = password;
        }

        public string[] DirectoryListSimple(string directory)
        {
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
                        MessageBox.Show(ex.Message);
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
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return new[] { "" };
        }
    }
}