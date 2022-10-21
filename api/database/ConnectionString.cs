namespace api.database
{
    public class ConnectionString
    {
        public string cs {get; set;}

        public ConnectionString(){
            string server = "cwe1u6tjijexv3r6.cbetxkdyhwsb.us-east-1.rds.amazonaws.com";
            string database = "mxhn6iycpf2s0ogy";
            string port = "3306";
            string userName = "xwh8dqhf8gwf6axc";
            string password = "zo3swg9r2kuci21q";


            cs = $@" server = {server}; user = {userName}; database = {database}; port = {port}; password = {password}; Allow User Variables=True; Convert Zero Datetime=true";
        }
    }
}