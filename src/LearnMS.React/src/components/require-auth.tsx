import { Permission, useProfileQuery } from "@/api/profile-api";
import LoadingPage from "@/pages/shared/loading-page";
import PermissionDeniedPage from "@/pages/shared/permission-denied-page";
import { Navigate } from "react-router-dom";

interface RequireAuthProps {
  children: JSX.Element;
  roles: ("Student" | "Teacher" | "Assistant")[];
  permissions?: Permission[];
}

const RequireAuth: React.FC<RequireAuthProps> = ({
  children,
  roles,
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
    !roles.includes(profile.role) &&
    profile.role === "Student"
  ) {
    return <Navigate to='/' />;
  }

  if (
    profile?.isAuthenticated &&
    !roles.includes(profile.role) &&
    (profile.role === "Teacher" || profile.role === "Assistant")
  ) {
    return <Navigate to='/dashboard' />;
  }

  if (
    profile?.isAuthenticated &&
    profile.role === "Assistant" &&
    permissions &&
    !permissions.every((permission) =>
      profile.permissions.includes(permission as any)
    )
  ) {
    return <PermissionDeniedPage permissions={permissions} />;
  }

  return children;
};

export default RequireAuth;
