import { Outlet } from "react-router-dom";
import Sidebar from "./Sidebar";
import Header from "./Header";
import { Container } from "reactstrap";
import { useEffect } from "react";


const FullLayout = ({ children }) => {
    useEffect(() => {
        window.addEventListener('scroll', isSticky);
        return () => {
            window.removeEventListener('scroll', isSticky);
        };
    });
    const isSticky = (e) => {
        const header = document.querySelector('.sticky-section');
        const scrollTop = window.scrollY;
        scrollTop >= 1 ? header.classList.add('is-sticky') : header.classList.remove('is-sticky');
    };
    return (
        <main>
            <div className="layout-Container">
                {/********header**********/}
                <div className="header sticky-section" isSticky><Header /> </div>
                <div className="pageWrapper d-lg-flex">
                    {/********Sidebar**********/}
                    {/********Content Area**********/}
                    <aside className="sidebarArea shadow" id="sidebarArea">
                        <Sidebar />
                    </aside>
                    <div className="contentArea">
                        {/********Middle Content**********/}
                        <Container className="p-4 wrapper" fluid>
                                {children}
                           {/* <Outlet/>*/}
                            </Container>
                        </div>
                </div>
            </div>
        </main>
    );
};

export default FullLayout;
