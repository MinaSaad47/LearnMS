import { Button } from "@/components/ui/button";
import { Link } from "react-router-dom";

const NavBar = () => {
  return (
    <div className='top-0 left-0 flex items-start justify-center w-full gap-2 py-4 shadow-2xl rounded-b-2xl'>
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
      <Button
        variant='outline'
        className='transition-all duration-500 border-0 hover:text-white hover:bg-blue-400'>
        Payments
      </Button>
      <Button
        variant='outline'
        className='transition-all duration-500 border-0 hover:text-white hover:bg-blue-400'>
        Profile
      </Button>
    </div>
  );
};

export default NavBar;
