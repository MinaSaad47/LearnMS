import { RedeemRequest, useRedeemMutation } from "@/api/credits-api";
import { useProfileQuery } from "@/api/profile-api";
import Footer from "@/components/footer";
import Loading from "@/components/loading/loading";
import { Button } from "@/components/ui/button";
import { Card, CardContent, CardTitle } from "@/components/ui/card";
import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormMessage,
} from "@/components/ui/form";
import { Input } from "@/components/ui/input";
import { toast } from "@/components/ui/use-toast";
import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";
import { Navigate } from "react-router-dom";

const StudentPayments = () => {
  const { profile, isLoading } = useProfileQuery();
  const redeemMutation = useRedeemMutation();

  const redeemForm = useForm({
    resolver: zodResolver(RedeemRequest),
    values: {
      code: "",
    },
  });

  if (profile?.isAuthenticated == false) {
    return <Navigate to='/sign-in-sign-up' />;
  }

  if (isLoading) {
    return (
      <div className='flex items-center justify-center w-full h-full'>
        <Loading />
      </div>
    );
  }

  const onSubmit = (data: RedeemRequest) => {
    redeemMutation.mutate(data, {
      onSuccess: (data) => {
        toast({
          title: "Redeem successful",
          description: `added ${data.data.value} LE to your credits`,
        });
      },
      onError: () => {
        toast({
          title: "Redeem failed",
          description: "Please try again",
          variant: "destructive",
        });
      },
    });
  };

  return (
    <div className='flex flex-col items-center justify-center w-full h-full gap-4'>
      <Card className='w-[80%] p-2 my-10  text-white bg-blue-400'>
        <CardTitle>Redeem Credits</CardTitle>
        <p>Redeem credits to purchase courses</p>
        <CardContent className='p-4'>
          <Form {...redeemForm}>
            <form
              className='flex gap-4'
              onSubmit={redeemForm.handleSubmit(onSubmit)}>
              <FormField
                control={redeemForm.control}
                name='code'
                render={({ field }) => (
                  <FormItem className='w-full'>
                    <FormControl {...field}>
                      <Input
                        className='font-bold text-blue-700'
                        placeholder='Code'
                      />
                    </FormControl>
                    <FormMessage />
                  </FormItem>
                )}
              />
              <Button>Redeem</Button>
            </form>
          </Form>
        </CardContent>
      </Card>
      <Card className='flex items-baseline gap-2 p-2 text-white bg-slate-500'>
        <CardTitle>
          <h2 className='text-5xl'>Credit: </h2>
        </CardTitle>
        <CardContent>
          <span className='text-4xl text-center'>{profile.credits} LE</span>
        </CardContent>
      </Card>

      <div className='w-full mt-auto'>
        <Footer />
      </div>
    </div>
  );
};

export default StudentPayments;
