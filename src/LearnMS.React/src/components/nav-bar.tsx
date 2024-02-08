import { useLogoutMutation } from "@/api/auth-api";
import { useProfileQuery } from "@/api/profile-api";
import { Avatar, AvatarFallback, AvatarImage } from "@/components/ui/avatar";
import { Button } from "@/components/ui/button";
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuGroup,
  DropdownMenuItem,
  DropdownMenuLabel,
  DropdownMenuSeparator,
  DropdownMenuTrigger,
} from "@/components/ui/dropdown-menu";
import { getFirstCharacters } from "@/lib/utils";
import { useModalStore } from "@/store/use-modal-store";
import { LogOut, User } from "lucide-react";
import { Link } from "react-router-dom";

const NavBar = () => {
  const logoutMutation = useLogoutMutation();
  const { profile } = useProfileQuery();
  const { openModal } = useModalStore();

  return (
    <div className='top-0 left-0 flex items-start justify-around w-full gap-2 py-4 shadow-2xl rounded-b-2xl'>
      <div>
        <Link to='/'>
          <Button
            variant='outline'
            className='transition-all duration-500 border-0 hover:text-white hover:bg-blue-400'>
            Home
          </Button>
        </Link>
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

      {profile?.isAuthenticated && profile.role === "Student" && (
        <DropdownMenu>
          <DropdownMenuTrigger asChild>
            <Avatar className='transition-all duration-500 hover:cursor-pointer hover:scale-110'>
              <AvatarImage src={profile?.profilePicture} />
              <AvatarFallback className='text-white bg-blue-300'>
                {getFirstCharacters(profile?.fullName)}
              </AvatarFallback>
            </Avatar>
          </DropdownMenuTrigger>
          <DropdownMenuContent className='flex flex-col items-center text-white bg-blue-300 border-none shadow-2xl'>
            <DropdownMenuLabel className='font-normal text-blue-400 bg-blue-200 rounded'>
              {profile.email}
            </DropdownMenuLabel>
            <DropdownMenuSeparator />
            <DropdownMenuGroup className='flex flex-col items-center w-full'>
              <DropdownMenuItem
                onClick={() => openModal("profile-modal")}
                className='flex justify-center w-full hover:cursor-pointer hover:bg-white hover:text-blue-400'>
                <User /> Profile
              </DropdownMenuItem>
            </DropdownMenuGroup>
            <Button
              variant='destructive'
              size='icon'
              onClick={() => logoutMutation.mutate()}
              className='mt-4 transition-all duration-200 hover:scale-110'>
              <LogOut />
            </Button>
          </DropdownMenuContent>
        </DropdownMenu>
      )}
    </div>
  );
};

export default NavBar;
