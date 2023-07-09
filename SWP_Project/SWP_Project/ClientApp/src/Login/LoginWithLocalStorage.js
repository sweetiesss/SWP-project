import React, { useEffect,useState,useRef } from "react";
import Home from "./Home";
    
function LoginWithLocalStorage(){
    const [username,setUsername] = useState('');
    const [password,setPassword] = useState('');
    const getUsername=localStorage.getItem("usernameData")
    const getPassword = localStorage.getItem("passwordData")
    const [accountUsername, setAccountUserName] = useState('');

    //useEffect(() => {

    //    fetch('https://localhost:7219/api/students/' + accountId)
    //       .then(res => res.json())
    //        .then(stu => {
                        
    //        })
    //}, [accountUsername])
    const handleSubmit = (e) => {
        
            const data = new FormData(e.target)
            const result = Object.fromEntries(data.entries());
        setAccountUserName(result.username)
       
        e.preventDefault();
          
        
    }

    return(
        <div>
            {
                getUsername&&getPassword?
                <Home/>:
            <form onSubmit={handleSubmit}>
                <div>
                            <input type="text" name='username' value={username} onChange={(e)=> setUsername(e.target.value)} />
                </div>
                <div>
                            <input type="password" name='password' value={password} onChange={(e) => setPassword(e.target.value)} />
                </div>
                <button>Login</button>
            </form>
            }
        </div>
    );
}
export default LoginWithLocalStorage;