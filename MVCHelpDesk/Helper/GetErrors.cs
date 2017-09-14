using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCHelpDesk.Helper
{
    public class GetErrors : Controller
    {
        public Dictionary<string, object> GetErrorsFromModelState(ModelStateDictionary mState)
        {
            //explicar el errors
            var errors = new Dictionary<string, object>();
            foreach (var key in mState.Keys)
            {
                // Only send the errors to the client.
                if (mState[key].Errors.Count > 0)
                {
                    errors[key] = mState[key].Errors;
                }
                else
                {
                    errors[key] = "true";
                }
            }
            return errors;
        }
    }
}