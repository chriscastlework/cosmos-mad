using System;
using System.Collections.Generic;

namespace CosmosWizard.Web.AutoGeneratedFiles
{
    public class Risk
    {
        public class Items2
        {
        }

        public class Owner2
        {
            public Items2 Items;
            public object Id;
        }

        public class Action
        {
            public string Description;
            public DateTime DueDate;
            public string Bucket;
            public string Shard;
            public string Id;
        }

        public class Metadata
        {
        }

        public string id;
        public string _rid;
        public string _self;
        public int _ts;
        public string _etag;
        public string Shard;
        public bool HasOverDueActions;
        public int Seq;
        public string SiteId;
        public string Site;
        public string Title;
        public string Description;
        public string RiskCategoryId;
        public string RiskCategory;
        public string LikelihoodId;
        public string Likelihood;
        public string ImpactId;
        public string Impact;
        public string RiskLevelId;
        public string RiskLevel;
        public int RiskLevelScore;
        public string RiskStatusId;
        public string RiskStatus;
        public DateTime ReviewDate;
        public bool IsOwnedByCustomer;
        public bool IsPublished;
        public object ClosedByPerson;
        public string ExternalReference;
        public Owner2 Owner;
        public IList<Action> Actions;
        public object Assignee;
        public IList<object> Attachments;
        public IList<object> Buildings;
        public DateTime CreatedDateTime;
        public DateTime LastUpdatedDateTime;
        public string CreatedByPersonId;
        public object CreatedByPersonName;
        public string LastUpdatedByPersonId;
        public object LastUpdatedByPersonName;
        public string Bucket;
        public bool IsActive;
        public int _lsn;
        public Metadata _metadata;

    }
}
