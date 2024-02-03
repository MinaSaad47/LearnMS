import { useLogoutMutation } from "@/api/auth-api";
import { Button } from "@/components/ui/button";
import {
  File,
  GanttChartSquare,
  LogOut,
  QrCode,
  School,
  Shield,
  User,
} from "lucide-react";
import { Link } from "react-router-dom";
import { ModeToggle } from "./mode-toggle";

const SideBar = () => {
  const logoutMutation = useLogoutMutation();

  return (
    <div className='border-r border-rounded min-h-full flex flex-col gap-4 p-2 max-w-[16rem]'>
      <ModeToggle />
      <div className='space-y-2'>
        <h2 className='text-2xl font-bold'>Dashboard</h2>
        <div className='space-y-1'>
          <Button variant='ghost' className='inline-flex justify-start w-full'>
            <GanttChartSquare className='w-4 h-4 mr-2' />
            Summary
          </Button>
        </div>
      </div>

      <div className='space-y-2 '>
        <h2 className='text-2xl font-bold'>Materials</h2>
        <div className='space-y-1'>
          <Link to='/courses'>
            <Button
              variant='secondary'
              className='inline-flex justify-start w-full'>
              <School className='w-4 h-4 mr-2' />
              Courses
            </Button>
          </Link>
          <Link to='/credit-codes'>
            <Button
              variant='ghost'
              className='inline-flex justify-start w-full'>
              <QrCode className='w-4 h-4 mr-2' />
              Credit Codes
            </Button>
          </Link>
          <Link to='/files'>
            <Button
              variant='ghost'
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
          <Link to='/students'>
            <Button
              variant='ghost'
              className='inline-flex justify-start w-full'>
              <User className='w-4 h-4 mr-2' />
              Students
            </Button>
          </Link>
        </div>
        <div className='space-y-1'>
          <Button variant='ghost' className='inline-flex justify-start w-full'>
            <Shield className='w-4 h-4 mr-2' />
            Assistants
          </Button>
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

export default SideBar;
