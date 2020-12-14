﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using WkHtmlToXSharp;

namespace Xpdf
{
    public class XPdfer : XPdfBase, IDisposable
    {

        #region Attributes

        private bool disposed = false;

        private INativeLibraryBundle nativeBundle { get; set; }

        #endregion

        #region Public Methods

        public XPdfer()
        {
            if (nativeBundle == null)
            {
                if (WkHtmlToXLibrariesManager.RunningIn64Bits)
                {
                    nativeBundle = new Win64NativeBundle();
                }
                else
                {
                    nativeBundle = new Win32NativeBundle();
                }

                WkHtmlToXLibrariesManager.Register(nativeBundle);
            }
        }

        /// <summary>
        /// Call generator pdf from Url or Html
        /// </summary>
        /// <param name="url">html type</param>
        /// <param name="fileName">Name of file</param>
        /// <returns>byte[] from file</returns>
        public override byte[] UrlToPdf(Url url, string fileName)
        {
            byte[] datas = base.CreatePdfFile(url.Value, fileName, EnumPdfType.Url);
            return datas;
        }

        /// <summary>
        /// Call generator pdf from Url or Html
        /// </summary>
        /// <param name="html">html type</param>
        /// <param name="fileName">Name of file</param>
        /// <returns>byte[] from file</returns>
        public override byte[] HtmlToPdf(string html, string fileName)
        {
            return base.CreatePdfFile(html, fileName, EnumPdfType.Html);
        }

        #endregion
        
        #region Implement IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    //managed objects
                }
                // set fields to null
                disposed = true;
            }
        }

        ~XPdfer()
        {
            Dispose(false);
        }

        #endregion

    }
}
