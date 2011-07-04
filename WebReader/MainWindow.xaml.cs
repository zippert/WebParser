// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="Lokaldelen AB">
//   Mattias Z
// </copyright>
// <summary>
//   Interaction logic for MainWindow.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WebReader
{
    using System;
    using System.Windows;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void ParseButtonClick(object sender, RoutedEventArgs e)
        {
            var parser = new WebParser(urlField.Text);
            string result = null;
            try
            {
                result = parser.Parse();
            }
            catch (Exception exception)
            {
                result = exception.Message;
            }

            textBlock1.Text = result;
        }


    }
}
