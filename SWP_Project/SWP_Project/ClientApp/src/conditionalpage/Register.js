import { useRef, useState, useEffect } from 'react';
const USER_REGREX = /^[a-zA-Z][a-zA-Z0-9-_]{2,24}$/;
const EMAIL_REGREX = /^[a-zA-Z][a-zA-Z0-9-_]{2,24}@fpt.edu.vn$/;
const PASSWORD_REGREX = /^.* (?=.{ 8, })(?=.*\d *\d) (?=.* [a - zA - Z] * [a - zA - Z])(?!.*\W).* $/;
const Register = () => {
    const userRef = useRef();
    const errRef = useRef();
    const [user, setUser] = useState('');
    const [validUser, setValidUser] = useState(false);
    const [userFocus, setUserFocus] = useState(false);

    const [pwd, setPwd] = useState('');
    const [validPwd, setValidPwd] = useState(false);
    const [pwdFocus, setPwdFocus] = useState(false);

    const [matchPwd,setMatchPwd] = useState('');
    const [validMatch, setValidMatch] = useState(false);
    const [MatchFocus, setMatchFocus] = useState(false);

    const [errMsg, setErrMsg] = useState('');
    const [succes, setSucces] = useState(false);

    useEffect(() => {
        userRef.current.focus();
    },[])

    useEffect(() => {
        const result = USER_REGREX.test(user);
        console.log(result);
        console.log(user);
        setValidUser(user);
    },[user])

    useEffect(() => {
        const result = PASSWORD_REGREX.test(pwd);
        console.log(result);
        console.log(pwd);
        setValidPwd(result);
        const match = pwd === matchPwd;
        setValidMatch(match);
    }, [pwd,matchPwd])

    useEffect(() => {
        setErrMsg('');
    },[user,pwd,matchPwd])


    return (
        <section>
            <p ref={errRef} className={errMsg ? "errMsg" : "offscreen"} aria-live="assertive">{errMsg}</p>
            <h1>Register</h1>
            <form>
                <label htmlFor="username">Username:</label>
                <input type="text" id="username" ref={ }></input>
            </form>
        </section>
    )
}
export default Register