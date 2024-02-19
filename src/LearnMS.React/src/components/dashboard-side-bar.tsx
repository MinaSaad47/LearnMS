import { useLogoutMutation } from "@/api/auth-api";
import { Button } from "@/components/ui/button";
import { cn } from "@/lib/utils";
import { File, LogOut, QrCode, School, Shield, User } from "lucide-react";
import { Link, useLocation } from "react-router-dom";

const DashboardSideBar = () => {
  const logoutMutation = useLogoutMutation();

  const { pathname } = useLocation();

  const is = (path: string) => {
    return pathname.startsWith(`/dashboard/${path}`);
  };

  return (
    <div className='border-r border-rounded self-stretch  flex flex-col gap-4 p-2 max-w-[16rem]'>
      <div className='space-y-2 '>
        <h2 className='text-2xl font-bold'>Materials</h2>
        <div className='space-y-1'>
          <Link to='/dashboard/courses'>
            <Button
              variant={is("courses") ? "default" : "secondary"}
              className={cn("inline-flex justify-start w-full")}>
              <School className='w-4 h-4 mr-2' />
              Courses
            </Button>
          </Link>
          <Link to='/dashboard/credit-codes'>
            <Button
              variant={is("credit-codes") ? "default" : "secondary"}
              className='inline-flex justify-start w-full'>
              <QrCode className='w-4 h-4 mr-2' />
              Credit Codes
            </Button>
          </Link>
          <Link to='/dashboard/files'>
            <Button
              variant={is("files") ? "default" : "secondary"}
              className='inline-flex justify-start w-full'>
              <File className='w-4 h-4 mr-2' />
              Files
            </Button>
          </Link>
        </div>
      </div>

      <div className='space-y-2'>
        <h2 className='text-2xl font-bold'>Users</h2>
        <div className='space-y-1'>
          <Link to='/dashboard/students'>
            <Button
              variant={is("students") ? "default" : "secondary"}
              className='inline-flex justify-start w-full'>
              <User className='w-4 h-4 mr-2' />
              Students
            </Button>
          </Link>
        </div>
        <div className='space-y-1'>
          <Link to='/dashboard/assistants'>
            <Button
              variant={is("assistants") ? "default" : "secondary"}
              className='inline-flex justify-start w-full'>
              <Shield className='w-4 h-4 mr-2' />
              Assistants
            </Button>
          </Link>
        </div>
      </div>

      <div className='mt-auto'>
        <Button
          className='w-full'
          variant='destructive'
          onClick={() => logoutMutation.mutate()}>
          <LogOut className='w-4 h-4 mr-2' />
          Log out
        </Button>
      </div>
    </div>
  );
};

export default DashboardSideBar;
