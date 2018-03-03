using System.Collections.Generic;

namespace Community.Data
{
    public class OperationResultFutureVersion
    {
        private static readonly OperationResultFutureVersion _success = new OperationResultFutureVersion(true);

        /// <summary>
        ///     Failure constructor that takes error messages
        /// </summary>
        /// <param name="errors"></param>
        public OperationResultFutureVersion(params string[] errors) : this((IEnumerable<string>)errors)
        {
        }

        /// <summary>
        ///     Failure constructor that takes error messages
        /// </summary>
        /// <param name="errors"></param>
        public OperationResultFutureVersion(IEnumerable<string> errors)
        {
            if (errors == null)
            {
                Succeeded = true;
                return;
            }
            Succeeded = false;
            Errors = errors;
        }

        /// <summary>
        /// Constructor that takes whether the result is successful
        /// </summary>
        /// <param name="success"></param>
        protected OperationResultFutureVersion(bool success)
        {
            Succeeded = success;
            Errors = new string[0];
        }

        /// <summary>
        ///     True if the operation was successful
        /// </summary>
        public bool Succeeded { get; private set; }

        /// <summary>
        ///     List of errors
        /// </summary>
        public IEnumerable<string> Errors { get; private set; }

        /// <summary>
        ///     Static success result
        /// </summary>
        /// <returns></returns>
        public static OperationResultFutureVersion Success
        {
            get { return _success; }
        }

        /// <summary>
        ///     Failed helper method
        /// </summary>
        /// <param name="errors"></param>
        /// <returns></returns>
        public static OperationResultFutureVersion Failed(params string[] errors)
        {
            return new OperationResultFutureVersion(errors);
        }
    }
}
