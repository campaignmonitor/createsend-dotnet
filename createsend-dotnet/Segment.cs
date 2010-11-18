using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace createsend_dotnet
{
    public class Segment
    {
        private string _segmentID;

        public Segment(string segmentID)
        {
            _segmentID = segmentID;
        }

        public static string Create(string listID, string title, SegmentRules rules)
        {
            string json = "";
            try
            {
                json = HttpHelper.Post(string.Format("/segments/{0}.json", listID), null, JavaScriptConvert.SerializeObject(
                    new Dictionary<string, object>() { { "ListID", listID }, { "Title", title }, { "Rules", rules } })
                    );
            }
            catch (CreatesendException ex)
            {
                ErrorResult<RuleErrorResults> result = JavaScriptConvert.DeserializeObject<ErrorResult<RuleErrorResults>>(ex.ResponseData);

                //TODO : return this result instead of newly created segment id - example code gonna need to show how to handle this
            }
            catch (Exception ex) { throw ex; }
            
            return JavaScriptConvert.DeserializeObject<string>(json);
        }

        public SegmentDetail Details()
        {
            string json = HttpHelper.Get(string.Format("/segments/{0}.json", _segmentID), null);
            return JavaScriptConvert.DeserializeObject<SegmentDetail>(json);
        }
    }
}
