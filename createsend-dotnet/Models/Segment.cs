using System;
using System.Collections.Generic;
using System.Text;

namespace createsend_dotnet
{
    public class BasicSegment
    {
        public string ListID { get; set; }
        public string SegmentID { get; set; }
        public string Title { get; set; }
    }

    public class Segments : List<BasicSegment>
    {
    }

    public class SegmentDetail : BasicSegment
    {
        public int ActiveSubscribers { get; set; }
        public SegmentRuleGroups RuleGroups { get; set; }
    }

    public class SegmentRules : List<Rule>
    {
    }

    public class Rule
    {
        public string RuleType { get; set; }
        public string Clause { get; set; }
    }

    public class SegmentRuleGroups : List<SegmentRuleGroup>
    {
    }

    public class SegmentRuleGroup
    {
        public SegmentRules Rules { get; set; }
    }

    public class RuleErrorResults : List<RuleErrorResult>
    {
    }

    public class RuleErrorResult
    {
        public string Subject { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
        public ClauseErrorResults ClauseResults { get; set; }
    }

    public class ClauseErrorResults : List<ClauseErrorResult>
    {
    }

    public class ClauseErrorResult
    {
        public string Clause { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
    }
}