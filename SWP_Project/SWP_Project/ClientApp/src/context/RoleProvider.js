import { createContext, useState } from 'react';

const RoleConext = createContext({});

export const RoleProvider = ({ children }) => {
    const [role, setRole] = useState({});
    return (
        <RoleConext.Provider value={{ role, setRole }}>
            {children}
        </RoleConext.Provider>
    )
}
export default RoleConext;