
import React, { useState, useEffect, useContext } from 'react';
import axios from 'axios';

const AuthContext = React.createContext();

const AuthContextComponent = ({ children }) => {
    const [user, setUser] = useState(null);
    const [isLoading, setIsLoading] = useState(true);

    useEffect(() => {
        const getUser = async () => {
            const { data } = await axios.get('/api/account/getcurrentuser');
            setUser(data);
            setIsLoading(false);
        }

        getUser();
    }, []);

    return (
        <AuthContext.Provider value={{ user, setUser }}>
            {isLoading ? <span></span> : children}
        </AuthContext.Provider>
    )
}

export const useAuthDataContext = () => useContext(AuthContext);

export default AuthContextComponent;