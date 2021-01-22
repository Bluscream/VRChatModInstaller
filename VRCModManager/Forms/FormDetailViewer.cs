﻿using System;
using System.Diagnostics;
using System.Windows.Forms;
using CommonMark;
using VRCModManager.DataModels;
using mshtml;
namespace VRCModManager
{
    public partial class FormDetailViewer : Form
    {
        private ReleaseInfo _release;
        public FormDetailViewer(ReleaseInfo release)
        {
            InitializeComponent();
            _release = release;
            labelTitle.Text = string.Format("{0} by {1} {2}", release.modName, release.modAuthor, release.modVersion);
            DescriptionHandler();
        }
        private void DescriptionHandler()
        {
            string description = CommonMarkConverter.Convert(_release.modDescription);
            webBrowserDescription.DocumentText = description;
        }
        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linkLabelDirectLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_release.modDownloadLink.StartsWith("https://"))
            {
                Process.Start(_release.modDownloadLink);
            }
        }

        private void webBrowserDescription_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            InitCSS();
        }

        // Add CSS styling to the webBrowser object
        private void InitCSS()
        {
            IHTMLDocument2 document = (webBrowserDescription.Document.DomDocument) as IHTMLDocument2;

            // The first parameter is the url, the second is the index of the added style sheet.
            IHTMLStyleSheet styleSheet = document.createStyleSheet("", 0);

            // Change the font for everything in the document. Font list taken from the Github readme page
            int index = styleSheet.addRule("*", "font-family: -apple-system,BlinkMacSystemFont,\"Segoe UI\",Helvetica,Arial,sans-serif,\"Apple Color Emoji\",\"Segoe UI Emoji\",\"Segoe UI Symbol\";");

            // Edit existing rules
            // styleSheet.cssText = @"h1 { color: blue; }";

            // Remove existing rules
            // styleSheet.removeRule(index);
        }
    }
}
