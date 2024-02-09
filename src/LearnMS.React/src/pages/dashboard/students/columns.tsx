import { useDeleteStudentMutation } from "@/api/students-api";
import Confirmation from "@/components/confirmation";
import { Avatar, AvatarFallback, AvatarImage } from "@/components/ui/avatar";
import { Button } from "@/components/ui/button";
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuLabel,
  DropdownMenuSeparator,
  DropdownMenuTrigger,
} from "@/components/ui/dropdown-menu";
import { useModalStore } from "@/store/use-modal-store";
import { Student } from "@/types/students";
import { ColumnDef } from "@tanstack/react-table";
import {
  ClipboardCopy,
  CreditCard,
  MailCheck,
  MoreHorizontal,
  Trash,
} from "lucide-react";

const levelMap = {
  Level0: "3rd Prep School",
  Level1: "1st Secondary School",
  Level2: "2nd Secondary School",
  Level3: "3rd Secondary School",
};

export const studentsColumns: ColumnDef<Student>[] = [
  {
    accessorKey: "profilePicture",
    header: "Profile Picture",
    cell: ({ row }) => {
      const student = row.original;
      return (
        <Avatar className='m-auto'>
          <AvatarImage src={student.profilePicture} alt='' />
          <AvatarFallback className='text-blue-500 bg-blue-200'>
            getFirstCharacters(student.fullName)
          </AvatarFallback>
        </Avatar>
      );
    },
    size: 90,
  },
  {
    accessorKey: "credit",
    header: "Credit",
    size: 50,
  },
  {
    accessorKey: "fullName",
    header: "Full Name",
  },
  {
    accessorKey: "email",
    header: "Email",
  },
  {
    accessorKey: "level",
    header: "Level",
    cell: ({ row }) => {
      const student = row.original;

      return levelMap[student.level];
    },
  },
  {
    accessorKey: "isVerified",
    header: "Verified",
    cell: ({ row }) => {
      const student = row.original;
      return student.isVerified ? (
        <MailCheck className='text-blue-500' />
      ) : (
        <MailCheck className='text-gray-400' />
      );
    },
    size: 50,
  },
  {
    id: "actions",
    enableHiding: false,
    size: 1,
    cell: ({ row }) => {
      const student = row.original;
      const { openModal } = useModalStore();
      const deleteStudentMutation = useDeleteStudentMutation();

      const onDeleting = () => {
        deleteStudentMutation.mutate({ id: student.id });
      };

      return (
        <DropdownMenu>
          <DropdownMenuTrigger asChild>
            <Button variant='ghost' className='w-8 h-8 p-0'>
              <span className='sr-only'>Open menu</span>
              <MoreHorizontal className='w-4 h-4' />
            </Button>
          </DropdownMenuTrigger>
          <DropdownMenuContent align='end'>
            <DropdownMenuLabel>Actions</DropdownMenuLabel>
            <DropdownMenuItem
              className='flex items-center gap-2 hover:cursor-pointer hover:bg-blue-600 hover:text-white'
              onClick={() => navigator.clipboard.writeText(student.id)}>
              <ClipboardCopy />
              Copy Student ID
            </DropdownMenuItem>
            <DropdownMenuSeparator />
            <DropdownMenuItem
              onClick={() => openModal("add-credit-modal", { student })}
              className='flex items-center gap-2 hover:cursor-pointer hover:bg-blue-600 hover:text-white'>
              <CreditCard />
              Add Credit
            </DropdownMenuItem>
            {/*
            <DropdownMenuItem className='flex items-center gap-2 hover:cursor-pointer hover:bg-blue-600 hover:text-white'>
              <MailCheck /> Verify Email
            </DropdownMenuItem>
            */}
            <div className='flex items-center w-full gap-2 hover:cursor-pointer hover:text-red-500'>
              <Confirmation
                button={
                  <Button
                    className='flex w-full gap-2 text-red-500 border-none hover:bg-red-500 hover:text-white'
                    variant='outline'>
                    <Trash className='w-4 h-4' />
                    Delete
                  </Button>
                }
                title='Are you sure you want to delete this student?'
                description='This action cannot be undone.'
                onConfirm={onDeleting}
              />
            </div>
          </DropdownMenuContent>
        </DropdownMenu>
      );
    },
  },
];
