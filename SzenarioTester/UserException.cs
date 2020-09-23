using System;

namespace SzenarioTester
{
    /// <summary>
    /// A type of exception that can be output to the user with
    /// then useful and localized information for the user.
    /// </summary>
    public class UserException : Exception
    {
        public UserException(string localizableText, Exception exception)
            : base(localizableText, exception)
        {
        }

        public string DisplayText => Loc(this.Message);

        /// <summary>
        /// Localizer method.
        /// </summary>
        /// <param name="text">The localizable text.</param>
        /// <returns>Localized version.</returns>
        private string Loc(string text)
        {
            // TODO: localize
            return text;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return DisplayText + base.ToString();
        }
    }
}