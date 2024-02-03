import { RegisterRequest, useRegisterMutation } from "@/api/auth-api";
import { useProfileQuery } from "@/api/profile-api";
import { Button } from "@/components/ui/button";
import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormMessage,
} from "@/components/ui/form";
import { Input } from "@/components/ui/input";
import { zodResolver } from "@hookform/resolvers/zod";
import { Loader2 } from "lucide-react";
import { useForm } from "react-hook-form";
import { FaFacebook, FaGoogle } from "react-icons/fa";
import { Link, Navigate, useNavigate } from "react-router-dom";

const RegisterPage = () => {
  const { profile } = useProfileQuery();

  const navigate = useNavigate();

  const registerMutation = useRegisterMutation();

  const form = useForm<RegisterRequest>({
    resolver: zodResolver(RegisterRequest),
    values: {
      email: "",
      password: "",
    },
  });

  if (profile?.isAuthenticated) {
    return <Navigate to='/' />;
  }

  const onSubmit = (data: RegisterRequest) => {
    registerMutation.mutate(data, {
      onSuccess: () => {
        navigate("/login");
      },
    });
  };

  return (
    <div className='flex items-center justify-center w-screen h-screen'>
      <div className='w-full h-full bg-zinc-900'></div>
      <div className='flex flex-col w-full h-full p-4'>
        <Link to={"/login"}>
          <Button className='ml-auto' variant='ghost'>
            Login
          </Button>
        </Link>
        <div className='flex flex-col items-center justify-center w-full h-full gap-4'>
          <Form {...form}>
            <form
              className='space-y-2 w-[80%]'
              onSubmit={form.handleSubmit(onSubmit)}>
              <FormField
                name='email'
                render={({ field }) => (
                  <FormItem>
                    <FormControl>
                      <Input placeholder='Email address' {...field} />
                    </FormControl>
                    <FormMessage />
                  </FormItem>
                )}
              />
              <FormField
                name='password'
                render={({ field }) => (
                  <FormItem>
                    <FormControl>
                      <Input
                        type='password'
                        placeholder='Password'
                        {...field}
                      />
                    </FormControl>
                    <FormMessage />
                  </FormItem>
                )}
              />
              <Button
                className='w-full'
                type='submit'
                disabled={registerMutation.isPending}>
                {registerMutation.isPending ? (
                  <Loader2 className='w-4 h-4 mr-2 animate-spin' />
                ) : (
                  "Register"
                )}
              </Button>

              <div className='flex items-center justify-center gap-2'>
                <div className='w-full h-px bg-zinc-700'></div>
                <span className='text-sm text-muted whitespace-nowrap'>
                  {"OR CONTINUE WITH"}
                </span>
                <div className='w-full h-px bg-zinc-700'></div>
              </div>
              <Button variant='outline' className='w-full'>
                <FaGoogle className='mr-2' /> Google
              </Button>
              <Button variant='outline' className='w-full'>
                <FaFacebook className='mr-2' /> Facebook
              </Button>
            </form>
          </Form>
        </div>
      </div>
    </div>
  );
};

export default RegisterPage;
