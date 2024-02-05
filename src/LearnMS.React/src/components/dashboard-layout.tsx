import { Separator } from "@/components/ui/separator";
import { Outlet } from "react-router-dom";
import DashboardSideBar from "./dashboard-side-bar";

export const DashboardLayout = () => {
  return (
    <div className='h-screen p-2'>
      <div className='flex items-start w-full h-full border rounded'>
        <DashboardSideBar />
        <Separator orientation='vertical' />
        <Outlet />
      </div>
    </div>
  );
};
