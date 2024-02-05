import { useProfileQuery } from "@/api/profile-api";
import LoadingPage from "@/pages/shared/loading-page";
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

  if (isLoading) {
    return <LoadingPage />;
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

  return children;
};

export default RequireAuth;
