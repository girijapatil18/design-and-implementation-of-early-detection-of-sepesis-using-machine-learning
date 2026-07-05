using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using diseasePrediction.DataSet1TableAdapters;

namespace diseasePrediction
{
    public class Class1
    {
        //class which contains members and functions
        tblAdminTableAdapter adminObj = new tblAdminTableAdapter();
        tblMembersTableAdapter memberObj = new tblMembersTableAdapter();
        tblTreatmentTableAdapter treatmentObj = new tblTreatmentTableAdapter();

        tblTopicsTableAdapter topicObj = new tblTopicsTableAdapter();
        tblCommentsTableAdapter commentObj = new tblCommentsTableAdapter();

        //admin login
        public bool CheckAdminLogin(string adminId, string pwd)
        {
            int cnt = int.Parse(adminObj.CheckAdminLogin(adminId, pwd).ToString());

            if (cnt == 1)

                return true;

            else

                return false;
        }

        //admin update password
        public void AdminUpdatePassword(string pwd, string adminId)
        {
            adminObj.UpdateAdminPassword(pwd, adminId);
        }

        //get admin details
        public DataTable GetAdminById(string adminId)
        {
            return adminObj.GetAdminById(adminId);
        }

        //member registration
        public void InsertMember(string memberId, string pwd, string name, string mobile, string emailId, DateTime date)
        {
            memberObj.InsertMember(memberId, pwd, name, mobile, emailId, date);
        }

        //member update
        public void UpdateMember(string name, string mobile, string emailId, string memberId)
        {
            memberObj.UpdateMember(name, mobile, emailId, memberId);
        }

        //member update password
        public void MemberUpdatePassword(string pwd, string adminId)
        {
            memberObj.UpdateMemberPassword(pwd, adminId);
        }

        //delete member
        public void DeleteMember(string memberId)
        {
            memberObj.DeleteMember(memberId);
        }

        //get member details
        public DataTable GetMemberById(string memberId)
        {
            return memberObj.GetMemberById(memberId);
        }

        //check member id
        public bool CheckMemberId(string memberId)
        {
            int cnt = int.Parse(memberObj.CheckMemberId(memberId).ToString());

            if (cnt == 1)

                return false;

            else

                return true;
        }

        //check memberlogin
        public bool CheckMemberLogin(string memId, string pwd)
        {
            int cnt = int.Parse(memberObj.CheckMemberLogin(memId, pwd).ToString());

            if (cnt == 1)

                return true;

            else

                return false;
        }

        //get all members
        public DataTable GetAllMembers()
        {
            return memberObj.GetData();
        }

        //get new members
        public DataTable GetNewMembers(DateTime sDate, DateTime eDate)
        {
            return memberObj.GetNewMembers(sDate, eDate);
        }

        //get treatment
        public DataTable GetTreatmentByDisease(string disease)
        {
            return treatmentObj.GetTreatmentByDisease(disease);
        }

        #region ----- Discussion Forum -----

        //function to insert new topic
        public void InsertTopic(string memberId, string topic, string postedDate)
        {
            topicObj.InsertTopic(memberId, topic, postedDate);
        }

        //function to get topic based on id
        public DataTable GetTopicById(int topicId)
        {
            return topicObj.GetTopicById(topicId);
        }

        //function to get all topics
        public DataTable GetAllTopics()
        {
            return topicObj.GetData();
        }

        //function to get topics based on member id
        public DataTable GetTopicsByMemberId(string memberId)
        {
            return topicObj.GetTopicsByMemberId(memberId);
        }

        //function to delete the topic
        public void DeleteTopic(int topicId)
        {
            topicObj.DeleteTopic(topicId);
        }

        //function to insert new comment
        public void InsertComment(int topicId, string memberId, string comment, string date)
        {
            commentObj.InsertComment(topicId, memberId, comment, date);
        }

        //function to get comment based on id
        public DataTable GetCommentById(int commentId)
        {
            return commentObj.GetCommentById(commentId);
        }

        //function to delete comment
        public void DeleteComment(int commentId)
        {
            commentObj.DeleteComment(commentId);
        }

        //function to delete topic comments
        public void DeleteTopicComments(int topicId)
        {
            commentObj.DeleteTopicComments(topicId);
        }

        //function to get topic comments
        public DataTable GetTopicComments(int topicId)
        {
            return commentObj.GetTopicComments(topicId);
        }

        //function to delete member comments
        public void DeleteMemberComments(string memberId)
        {
            commentObj.DeleteMemberComments(memberId);
        }

        //function to delete member topics
        public void DeleteMemberTopics(string memberId)
        {
            topicObj.DeleteMemberTopics(memberId);
        }

        #endregion
    }
}