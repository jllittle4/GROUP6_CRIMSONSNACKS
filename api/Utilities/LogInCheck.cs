using api.Interfaces;
using api.database;
using MySql.Data.MySqlClient;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;


namespace api.Utilities
{
    public class LogInCheck
    {

        public bool valid {get; set;}
        public ViewAllUsers readObject = new ViewAllUsers();
        // public List<User> users = readObject.GetAllUsers();

        public User temp = new User();



        // public bool CompareTo(){

        // }
        public void FindUser(){ //searches for the driver the user inputs
            List<User> users = readObject.GetAllUsers();
            User returnVal = new User();

            returnVal = users.Find(x => x.UserName == temp.UserName);

            
            if(returnVal.UserName == temp.UserName){
                CheckPassword();
            }
            else{
                System.Console.WriteLine("Username not found, please try again");
                
                valid = false;
            }   

        }
        public void CheckPassword(){
            List<User> users = readObject.GetAllUsers();
            User returnVal = users.Find(x => x.Password == temp.Password);


            if(returnVal.Password == temp.Password){
                System.Console.WriteLine("Login Successful");
                valid = true;
            }
            else{
                System.Console.WriteLine("Username not found, please try again");
                valid = false;
            }


        }
    }




}