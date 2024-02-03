import NavBar from "@/components/nav-bar";
import { useTheme } from "@/components/theme-provider";
import { useEffect } from "react";
import { Outlet } from "react-router-dom";

const StudentLayout = () => {
  const { setTheme } = useTheme();

  useEffect(() => setTheme("light"), []);

  return (
    <div className='flex flex-col w-screen h-screen'>
      <NavBar />
      <Outlet />
    </div>
  );
};

export default StudentLayout;
