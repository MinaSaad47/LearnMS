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
import { Sheet, SheetContent, SheetTrigger } from "@/components/ui/sheet";
import { cn, getFirstCharacters } from "@/lib/utils";
import { useModalStore } from "@/store/use-modal-store";
import { LogOut, MoreHorizontal, User } from "lucide-react";
import { Link, useLocation } from "react-router-dom";

const NavBar = () => {
  const logoutMutation = useLogoutMutation();
  const { profile } = useProfileQuery();
  const { pathname } = useLocation();
  const { openModal } = useModalStore();

  return (
    <div className='top-0 left-0 z-50 flex items-center justify-around w-full gap-2 py-4 shadow-2xl bg-white/20 font-signika rounded-b-2xl'>
      <div className='flex items-center justify-center gap-2'>
        <div className='hidden object-fill w-20 h-20 md:block'>
          <img className='w-full h-full' src='/logo.png' alt='' />
        </div>
        <div className='hidden space-x-3 font-bold md:block'>
          <Link to='/'>
            <Button
              variant='outline'
              className={cn(
                "transition-all text-xl bg-transparent  duration-500 border-0 rounded-full hover:text-white hover:bg-color2",
                pathname === "/" && "text-white bg-color2"
              )}>
              Home
            </Button>
          </Link>
          <Link to='/courses'>
            <Button
              variant='outline'
              className={cn(
                "transition-all text-xl bg-transparent duration-500 border-0 rounded-full hover:text-white hover:bg-color2",
                pathname === "/courses" && "text-white bg-color2"
              )}>
              Courses
            </Button>
          </Link>
          <Link to='/payments'>
            <Button
              variant='outline'
              className={cn(
                "transition-all text-xl bg-transparent duration-500 border-0 rounded-full hover:text-white hover:bg-color2",
                pathname === "/payment" && "text-white bg-color2"
              )}>
              Payments
            </Button>
          </Link>
        </div>

        <Sheet>
          <SheetTrigger className='md:hidden'>
            <MoreHorizontal />
          </SheetTrigger>
          <SheetContent className='flex flex-col items-center gap-2 pt-10'>
            <div className='w-36 h-36'>
              <img className='w-full h-full' src='/logo.png' alt='' />
            </div>
            <Link className='w-full' to='/'>
              <Button
                variant='outline'
                className='w-full transition-all duration-500 border-0 hover:text-white hover:bg-color2'>
                Home
              </Button>
            </Link>
            <Link className='w-full' to='/courses'>
              <Button
                variant='outline'
                className='w-full transition-all duration-500 border-0 hover:text-white hover:bg-color2'>
                Courses
              </Button>
            </Link>
            <Link className='w-full' to='/payments'>
              <Button
                variant='outline'
                className='w-full transition-all duration-500 border-0 hover:text-white hover:bg-color2'>
                Payments
              </Button>
            </Link>
          </SheetContent>
        </Sheet>
      </div>

      {profile?.isAuthenticated && profile.role === "Student" ? (
        <div className='flex items-center gap-2'>
          <p className='flex items-center justify-center gap-2 p-2 font-bold rounded text-color1'>
            {"Credit: "} {profile?.credits} {" LE"}
          </p>
          <DropdownMenu>
            <DropdownMenuTrigger asChild>
              <Avatar className='transition-all duration-500 hover:cursor-pointer hover:scale-110'>
                <AvatarImage src={profile?.profilePicture} />
                <AvatarFallback className='text-white bg-color2'>
                  {getFirstCharacters(profile?.fullName)}
                </AvatarFallback>
              </Avatar>
            </DropdownMenuTrigger>
            <DropdownMenuContent className='flex flex-col items-center text-white border-none shadow-2xl bg-color2'>
              <DropdownMenuLabel className='font-normal text-white rounded bg-color2'>
                {profile.email}
              </DropdownMenuLabel>
              <DropdownMenuSeparator />
              <DropdownMenuGroup className='flex flex-col items-center w-full'>
                <DropdownMenuItem
                  onClick={() => openModal("profile-modal")}
                  className='flex justify-center w-full hover:cursor-pointer hover:bg-white hover:text-color2'>
                  <User /> Profile
                </DropdownMenuItem>
              </DropdownMenuGroup>
              <Button
                size='icon'
                variant='outline'
                onClick={() => logoutMutation.mutate()}
                className='mt-4 transition-all duration-200 bg-color1 hover:scale-110'>
                <LogOut />
              </Button>
            </DropdownMenuContent>
          </DropdownMenu>
        </div>
      ) : (
        <div></div>
      )}
    </div>
  );
};

export default NavBar;
