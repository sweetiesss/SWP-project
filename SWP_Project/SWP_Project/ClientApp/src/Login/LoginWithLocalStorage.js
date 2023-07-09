import React, { useEffect,useState,useRef } from "react";
import Home from "./Home";
    
function LoginWithLocalStorage(props){
    const [username, setUsername] = React.useState('');
    const [password, setPassword] = React.useState('');
    const getUsername=localStorage.getItem("usernameData")
    const getPassword = localStorage.getItem("passwordData")
    const [accountUsername, setAccountUserName] = React.useState('')
    const [accountArray, setAccountArray] = React.useState()


    useEffect(() => {
        (async () => {
            const data = await fetch('https://localhost:7219/api/accounts')
                .then(res => res.json())
                .then(tasks => {
                    setAccountArray(tasks)
                })
        })()
    }, []);
    console.log(accountArray)
    const handleSubmit = (e) => {
            const data = new FormData(e.target)
            const result = Object.fromEntries(data.entries());
        for (var i = 0; i < accountArray.length; i++) {
            if (accountArray[i].username === result.username && accountArray[i].password === result.password) {
                console.log((accountArray[i].studentId));
                if (accountArray[i].studentId === null)
                break;
                props.updateLoginStatus(true);
                
                props.updateAccount(accountArray[i].studentId);
                break;
            }
        }
               
  
        e.preventDefault();
    }

    return(
        <div>
            {
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