import { useProfileQuery } from "@/api/profile-api";
import { Loader2 } from "lucide-react";
import { Navigate } from "react-router-dom";

interface RequireAuthProps {
  children: JSX.Element;
  role?: "Student" | "Teacher" | "Assistant";
  permissions?: string[];
}

const RequireAuth: React.FC<RequireAuthProps> = ({
  children,
  role,
  permissions,
}) => {
  const { profile, isError, isLoading, isFetching } = useProfileQuery();

  console.log(profile);

  if (isLoading) {
    return (
      <div className='flex items-center justify-center w-screen h-screen'>
        <Loader2 className='w-10 h-10 animate-spin' />
      </div>
    );
  }

  if (!isFetching && (isError || !profile?.isAuthenticated)) {
    return <Navigate to='/sign-in-sign-up' />;
  }

  if (
    profile?.isAuthenticated &&
    role !== profile.role &&
    profile.role === "Student"
  ) {
    return <Navigate to='/' />;
  }

  if (
    profile?.isAuthenticated &&
    role !== profile.role &&
    profile.role === "Teacher"
  ) {
    return <Navigate to='/dashboard' />;
  }

  console.log({ profile, role });

  return children;
};

export default RequireAuth;
