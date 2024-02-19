import {
  UpdateProfileRequest,
  useProfileQuery,
  useUpdateProfileMutation,
} from "@/api/profile-api";
import { Avatar, AvatarFallback, AvatarImage } from "@/components/ui/avatar";
import {
  Dialog,
  DialogContent,
  DialogDescription,
  DialogHeader,
  DialogTitle,
} from "@/components/ui/dialog";
import {
  Form,
  FormControl,
  FormDescription,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "@/components/ui/form";
import { Input } from "@/components/ui/input";
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "@/components/ui/select";
import { Tabs, TabsContent, TabsList, TabsTrigger } from "@/components/ui/tabs";
import { toast } from "@/components/ui/use-toast";
import { getFirstCharacters } from "@/lib/utils";
import { zodResolver } from "@hookform/resolvers/zod";
import React from "react";
import { useForm } from "react-hook-form";

interface ProfileModalProps {
  onClose: () => void;
}

const ProfileModal: React.FC<ProfileModalProps> = ({ onClose }) => {
  const { profile } = useProfileQuery();

  if (!profile?.isAuthenticated || profile.role !== "Student") {
    return;
  }

  return (
    <Dialog open onOpenChange={onClose}>
      <DialogContent className='border-none'>
        <DialogHeader>
          <DialogTitle className='flex items-center gap-2'>
            <Avatar className='transition-all duration-500 hover:cursor-pointer hover:scale-110'>
              <AvatarImage src={profile?.profilePicture} />
              <AvatarFallback className='text-white bg-color2'>
                {getFirstCharacters(profile?.fullName)}
              </AvatarFallback>
            </Avatar>
            Profile
          </DialogTitle>
          <DialogDescription>
            Contact assistant to edit your profile
          </DialogDescription>
        </DialogHeader>
        <Tabs defaultValue='account'>
          <TabsList>
            <TabsTrigger value='account'>Account</TabsTrigger>
            <TabsTrigger value='contact'>Contact</TabsTrigger>
          </TabsList>
          <TabsContent value='account'>
            <ProfileContent />
          </TabsContent>
          <TabsContent value='contact'>
            <ContactContent />
          </TabsContent>
        </Tabs>
      </DialogContent>
    </Dialog>
  );
};

function ProfileContent() {
  const { profile: user } = useProfileQuery();
  const updateProfileMutation = useUpdateProfileMutation();

  const profile =
    user?.isAuthenticated && user.role === "Student" ? user : null;

  const form = useForm<UpdateProfileRequest>({
    resolver: zodResolver(UpdateProfileRequest),
    values: {
      fullName: profile?.fullName ?? "",
      level: profile?.level ?? "Level0",
      school: profile?.school ?? "",
    },
    defaultValues: {
      fullName: profile?.fullName ?? "",
      level: profile?.level ?? "Level0",
      school: profile?.school ?? "",
    },
  });

  if (!profile) {
    return;
  }

  const onSubmit = (data: UpdateProfileRequest) => {
    updateProfileMutation.mutate(data, {
      onSuccess: () => {
        toast({
          title: "Profile updated",
          description: "Your profile has been updated",
        });
      },
    });
  };

  return (
    <Form {...form}>
      <form
        onSubmit={form.handleSubmit(onSubmit)}
        className='p-4 text-white bg-color2 rounded-2xl'>
        <fieldset disabled={true || updateProfileMutation.isPending}>
          <FormField
            name='fullName'
            render={({ field }) => (
              <FormItem>
                <FormLabel className='font-bold'>Full Name</FormLabel>
                <FormControl>
                  <Input className='text-color2' type='text' {...field} />
                </FormControl>
                <FormDescription className='text-sm text-white text-muted'>
                  Your full name
                </FormDescription>
                <FormMessage />
              </FormItem>
            )}
          />
          <FormField
            name='school'
            render={({ field }) => (
              <FormItem>
                <FormLabel className='font-bold'>School</FormLabel>
                <FormControl>
                  <Input className='text-color2' type='text' {...field} />
                </FormControl>
                <FormDescription className='text-sm text-white text-muted'>
                  Your school
                </FormDescription>
                <FormMessage />
              </FormItem>
            )}
          />

          <FormField
            name='level'
            render={({ field }) => (
              <FormItem>
                <FormLabel className='font-bold'>Level</FormLabel>
                <Select
                  onValueChange={field.onChange}
                  defaultValue={field.value}>
                  <FormControl>
                    <SelectTrigger className='text-color2'>
                      <SelectValue placeholder='Select your level' />
                    </SelectTrigger>
                  </FormControl>
                  <SelectContent className='text-color2'>
                    <SelectItem value='Level0'>3rd Prep School</SelectItem>
                    <SelectItem value='Level1'>1st Secondary School</SelectItem>
                    <SelectItem value='Level2'>2st Secondary School</SelectItem>
                    <SelectItem value='Level3'>3st Secondary School</SelectItem>
                  </SelectContent>
                </Select>
                <FormDescription className='text-sm text-white text-muted'>
                  Your level in school
                </FormDescription>
                <FormMessage />
              </FormItem>
            )}
          />
          {/*
          <Button className='w-full mt-10 bg-color2 hover:bg-white hover:text-color2'>
            <SaveAll /> Update
          </Button>
            */}
        </fieldset>
      </form>
    </Form>
  );
}

function ContactContent() {
  const { profile: user } = useProfileQuery();
  const updateProfileMutation = useUpdateProfileMutation();

  const profile =
    user?.isAuthenticated && user.role === "Student" ? user : null;

  const form = useForm<UpdateProfileRequest>({
    resolver: zodResolver(UpdateProfileRequest),
    values: {
      phoneNumber: profile?.phoneNumber ?? "",
      parentPhoneNumber: profile?.parentPhoneNumber ?? "",
      studentCode: profile?.studentCode ?? "",
    },
    defaultValues: {
      phoneNumber: profile?.phoneNumber ?? "",
      parentPhoneNumber: profile?.parentPhoneNumber ?? "",
      studentCode: profile?.studentCode ?? "",
    },
  });

  if (!profile) {
    return;
  }

  const onSubmit = (data: UpdateProfileRequest) => {
    updateProfileMutation.mutate(data, {
      onSuccess: () => {
        toast({
          title: "Profile updated",
          description: "Your profile has been updated",
        });
      },
    });
  };

  return (
    <Form {...form}>
      <form
        onSubmit={form.handleSubmit(onSubmit)}
        className='p-4 text-white bg-color2 rounded-2xl'>
        <fieldset disabled={true || updateProfileMutation.isPending}>
          <FormField
            name='phoneNumber'
            render={({ field }) => (
              <FormItem>
                <FormLabel className='font-bold'>Phone Number</FormLabel>
                <FormControl>
                  <Input className='text-color2' type='text' {...field} />
                </FormControl>
                <FormDescription className='text-sm text-white text-muted'>
                  We will use this phone number to contact you
                </FormDescription>
                <FormMessage />
              </FormItem>
            )}
          />
          <FormField
            name='parentPhoneNumber'
            render={({ field }) => (
              <FormItem>
                <FormLabel className='font-bold'>Parent Phone Number</FormLabel>
                <FormControl>
                  <Input className='text-color2' type='text' {...field} />
                </FormControl>
                <FormDescription className='text-sm text-white text-muted'>
                  We will use this phone number to contact you
                </FormDescription>
                <FormMessage />
              </FormItem>
            )}
          />
          <FormField
            name='studentCode'
            render={({ field }) => (
              <FormItem>
                <FormLabel className='font-bold'>Student Code</FormLabel>
                <FormControl>
                  <Input className='text-color2' type='text' {...field} />
                </FormControl>
                <FormDescription className='text-sm text-white text-muted'>
                  We will use this Code 
                </FormDescription>
                <FormMessage />
              </FormItem>
            )}
          />
          {/*
          <Button className='w-full mt-10 bg-color2 hover:bg-white hover:text-color2'>
            <SaveAll /> Update
          </Button>
            */}
        </fieldset>
      </form>
    </Form>
  );
}

export default ProfileModal;
