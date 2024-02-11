import {
  LoginRequest,
  RegisterRequest,
  useLoginMutation,
  useRegisterMutation,
} from "@/api/auth-api";
import { useProfileQuery } from "@/api/profile-api";
import { toast } from "@/components/ui/use-toast";
import { cn } from "@/lib/utils";
import LoadingPage from "@/pages/shared/loading-page";
import { useModalStore } from "@/store/use-modal-store";
import { zodResolver } from "@hookform/resolvers/zod";
import { Controller, useForm } from "react-hook-form";
import { Navigate } from "react-router-dom";
import "./auth.scss";

const SignInSignUpPage = () => {
  const { openModal } = useModalStore();

  const { profile, isFetching: profileFetching } = useProfileQuery();

  const registerMutation = useRegisterMutation();
  const registerFrom = useForm<RegisterRequest>({
    resolver: zodResolver(RegisterRequest),
    values: {
      email: "",
      password: "",
      confirmPassword: "",
      phoneNumber: "",
      fullName: "",
      parentPhoneNumber: "",
      school: "",
      level: "Level0",
    },
  });
  const registerErrors = registerFrom.formState.errors;
  const onRegister = (data: RegisterRequest) => {
    console.log(data);
    registerMutation.mutate(data, {
      onSuccess: (data) => {
        toast({
          title: "Register successful",
          description: data.message,
        });
      },
      onError: () => {},
    });
  };

  const loginMutation = useLoginMutation();
  const loginFrom = useForm<LoginRequest>({
    resolver: zodResolver(LoginRequest),
    values: {
      email: "",
      password: "",
    },
  });
  const loginErrors = loginFrom.formState.errors;
  const onLogin = (data: LoginRequest) => {
    loginMutation.mutate(data, {
      onSuccess: () => {
        toast({
          title: "Login successful",
          description: "Welcome back!",
        });
        window.location.reload();
      },
    });
  };

  if (loginMutation.isPending || profileFetching) {
    return <LoadingPage />;
  }

  if (profile?.isAuthenticated) {
    console.log(`navigate to ${profile.role} page`);
    return <Navigate to={profile.role === "Teacher" ? "/dashboard" : "/"} />;
  }

  return (
    <div id='auth' className='relative'>
      <div className='absolute top-0 left-0 flex items-center justify-center h-16 gap-1 text-white -translate-x-[10px] md:translate-x-0 md:translate-y-0 md:top-2 md:left-2 md:h-28'>
        <img src='/auth-logo.png' className='w-auto h-full' alt='' />
        <h1 className='text-xl font-bold md:text-3xl'>Mr Rafik isaac</h1>
      </div>
      <div className='main'>
        <input type='checkbox' id='chk' aria-hidden='true' />
        <div className='signup'>
          <form
            className={cn(
              "flex flex-col items-center gap-6 h-fit",
              Object.keys(registerErrors).length > 0 && "gap-2"
            )}
            onSubmit={registerFrom.handleSubmit(onRegister)}>
            <label htmlFor='chk' aria-hidden='true' className='mb-4'>
              Sign up
            </label>

            <input
              type='text'
              className='m-0'
              placeholder='Full Name'
              {...registerFrom.register("fullName")}
            />
            {registerErrors?.fullName && (
              <p className='text-red-500 form-error'>
                {registerErrors?.fullName.message}
              </p>
            )}
            <input
              type='text'
              placeholder='Phone Number'
              {...registerFrom.register("phoneNumber")}
            />
            {registerErrors?.phoneNumber && (
              <p className='text-red-500 form-error'>
                {registerErrors?.phoneNumber.message}
              </p>
            )}
            <input
              type='text'
              placeholder="Parent's Phone Number not Koshary Abo Tarek"
              {...registerFrom.register("parentPhoneNumber")}
            />
            {registerErrors?.parentPhoneNumber && (
              <p className='text-red-500 form-error'>
                {registerErrors?.parentPhoneNumber.message}
              </p>
            )}
            <input
              type='text'
              placeholder='School'
              {...registerFrom.register("school")}
            />
            {registerErrors?.school && (
              <p className='text-red-500 form-error'>
                {registerErrors?.school.message}
              </p>
            )}
            <input
              type='email'
              placeholder='Email'
              {...registerFrom.register("email")}
            />
            {registerErrors?.email && (
              <p className='text-red-500 form-error'>
                {registerErrors?.email.message}
              </p>
            )}
            <input
              type='password'
              placeholder='Password'
              {...registerFrom.register("password")}
            />
            {registerErrors?.password && (
              <p className='text-red-500 form-error'>
                {registerErrors?.password.message}
              </p>
            )}
            <input
              type='password'
              placeholder='Confirm Password'
              {...registerFrom.register("confirmPassword")}
            />
            {registerErrors?.confirmPassword && (
              <p className='text-red-500 form-error'>
                {registerErrors?.confirmPassword.message}
              </p>
            )}
            <Controller
              render={({ field }) => (
                <select className='w-[80%] p-2 rounded' {...field}>
                  <option value={"Level0"}>3rd Prep School</option>
                  <option value={"Level1"}>1st Secondary</option>
                  <option value={"Level2"}>2nd Secondary</option>
                  <option value={"Level3"}>3rd Secondary</option>
                </select>
              )}
              control={registerFrom.control}
              name='level'
              defaultValue={"Level0"}
            />
            {registerErrors?.level && (
              <p className='text-red-500 form-error'>
                {registerErrors?.level.message}
              </p>
            )}
            <button disabled={registerMutation.isPending}>Sign Up</button>
          </form>
        </div>
        <div className='flex flex-col items-center login'>
          <form
            className='flex flex-col items-center justify-center gap-4'
            onSubmit={loginFrom.handleSubmit(onLogin)}>
            <label htmlFor='chk' aria-hidden='true' className='mb-4'>
              Log In
            </label>
            <input
              type='email'
              placeholder='Email'
              {...loginFrom.register("email")}
            />
            {loginErrors?.email && (
              <p className='text-red-500 form-error'>
                {loginErrors?.email.message}
              </p>
            )}
            <input
              type='password'
              placeholder='Password'
              {...loginFrom.register("password")}
            />
            {loginErrors?.password && (
              <p className='text-red-500 form-error'>
                {loginErrors?.password.message}
              </p>
            )}
            <button disabled={registerMutation.isPending}>Log In</button>
          </form>
          <button
            onClick={() => openModal("forgot-password-modal")}
            className='underline text-color2 forgot'>
            Forgot Password ?
          </button>
        </div>
      </div>
    </div>
  );
};

export default SignInSignUpPage;
