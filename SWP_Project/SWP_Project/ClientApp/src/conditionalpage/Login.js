import { useRef, useState, useEffect, useContext } from 'react';
import RoleConext from '../context/RoleProvider';


const Login = () => {
    const { setRole } = useContext(RoleConext);
    const userRef = useRef();
    const errRef = useRef();

    const [user, setUser] = useState('');
    const [pwd, setPwd] = useState('');
    const [errMsg, setErrMsg] = useState('');
    const [succes, setSucces] = useState(false);

    useEffect(() => {
        userRef.current.focus();
    }, [])

    useEffect(() => {
        setErrMsg('')
    }, [user,pwd])

    const handleSubmit = async (e) => {
        e.preventDefault();
        console.log(user, pwd);
        setUser('');
        setPwd('');
        setSucces(true);
    }

    return (
        <>
            {succes ? (<section>
                <h1>You are log in!!</h1>
                <br />
                <p>
                    <a href="#">Home</a>
                </p>
            </section>) :
                <section>
                    <p ref={errRef} className={errMsg ? "errMsg" : "offscreen"} aria-live="assertive">{errMsg}</p>
                    <h1>Sign In</h1>
                    <form onSubmit={handleSubmit}>
                        <label htmlFor="username">Username:</label>
                        <input
                            type="text"
                            id="username"
                            ref={userRef}
                            autoComplete="off"
                            onChange={(e) => setUser(e.target.value)}
                            value={user}
                            required
                        ></input>
                        <label htmlFor="password">Password:</label>
                        <input
                            type="password"
                            id="password"
                            onChange={(e) => setPwd(e.target.value)}
                            value={pwd}
                            required
                        ></input>
                        <button>Sign in</button>
                    </form>
                </section>
            }</>
    )
}
export default Login;