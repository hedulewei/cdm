namespace Common
{
    public class UserTransaction
    {
        public UserTransaction()
        {
            UserInfo = new PoliceUser();
        }
        public UserTransactionType UserTransactionType { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public PoliceUser UserInfo { get; set; }
        //public override string ToString()
        //{
        //    var perm = string.Empty;
        //    if (UserInfo.Permission == null)
        //        return
        //            string.Format(
        //                "type:{0},operator:{1},AuthorityLevel:{2},RealName:{3},UserName:{4},PoliceCode:{5},Disabled:{6},CountyCode:{7},UserRole:{8},Notation:{9},permission:{10}",
        //                UserTransactionType, UserName ?? "null", UserInfo.AuthorityLevel, UserInfo.RealName ?? "null",
        //                UserInfo.UserName ?? "null", UserInfo.PoliceCode ?? "null",
        //                UserInfo.Disabled, UserInfo.CountyCode ?? "null", UserInfo.UserRole,
        //                UserInfo.Notation ?? "null", perm);
        //    foreach (var v in UserInfo.Permission)
        //    {
        //        perm += v.Key + ":" + v.Value + ",";
        //    }
        //    return string.Format("type:{0},operator:{1},AuthorityLevel:{2},RealName:{3},UserName:{4},PoliceCode:{5},Disabled:{6},CountyCode:{7},UserRole:{8},Notation:{9},permission:{10}",
        //        UserTransactionType, UserName ?? "null", UserInfo.AuthorityLevel, UserInfo.RealName ?? "null", UserInfo.UserName ?? "null", UserInfo.PoliceCode ?? "null",
        //        UserInfo.Disabled, UserInfo.CountyCode ?? "null", UserInfo.UserRole,
        //       UserInfo.Notation ?? "null", perm);
        //   // return base.ToString();
        //}
    }
}