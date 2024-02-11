import React from "react";

interface PermissionDeniedPageProps {
  permissions: string[];
}

const PermissionDeniedPage: React.FC<PermissionDeniedPageProps> = ({
  permissions,
}) => {
  return (
    <div className='flex flex-col items-center justify-center w-full h-full gap-4'>
      <h1 className='text-3xl'>Permission Denied</h1>
      <p>You do not have permission to access this page</p>
      <div className='flex items-center justify-center'>
        {permissions.map((permission) => (
          <div className='text-sm text-red-600' key={permission}>
            {permission}
          </div>
        ))}
      </div>
    </div>
  );
};

export default PermissionDeniedPage;
