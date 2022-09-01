namespace APICODERHOUSE.Repository
{
    public class TokenHandler
    {
        public static int userToken { get; set; }
        public static int UpdateUserToken(int id)
        {
            userToken = id;
            return userToken;
        }
    }
}
