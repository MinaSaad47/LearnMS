import { Separator } from "@/components/ui/separator";
import { Outlet } from "react-router-dom";
import SideBar from "./side-bar";

export const Layout = () => {
  return (
    <div className='h-screen p-2'>
      <div className='flex items-start w-full h-full border rounded'>
        <SideBar />
        <Separator orientation='vertical' />
        <Outlet />
      </div>
    </div>
  );
};
