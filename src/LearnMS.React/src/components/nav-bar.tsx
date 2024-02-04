import { useLogoutMutation } from "@/api/auth-api";
import { Button } from "@/components/ui/button";
import { LogOut } from "lucide-react";
import { Link } from "react-router-dom";

const NavBar = () => {
  const logoutMutation = useLogoutMutation();

  return (
    <div className='top-0 left-0 flex items-start justify-around w-full gap-2 py-4 shadow-2xl rounded-b-2xl'>
      <div>
        <Button
          variant='outline'
          className='transition-all duration-500 border-0 hover:text-white hover:bg-blue-400'>
          Home
        </Button>
        <Link to='/courses'>
          <Button
            variant='outline'
            className='transition-all duration-500 border-0 hover:text-white hover:bg-blue-400'>
            Courses
          </Button>
        </Link>
        <Link to='/payments'>
          <Button
            variant='outline'
            className='transition-all duration-500 border-0 hover:text-white hover:bg-blue-400'>
            Payments
          </Button>
        </Link>
        <Button
          variant='outline'
          className='transition-all duration-500 border-0 hover:text-white hover:bg-blue-400'>
          Profile
        </Button>
      </div>
      <Button
        variant='destructive'
        size='icon'
        onClick={() => logoutMutation.mutate()}
        className='transition-all duration-500 hover:scale-105'>
        <LogOut />
      </Button>
    </div>
  );
};

export default NavBar;
