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
import { Student } from "@/types/students";
import { ColumnDef } from "@tanstack/react-table";
import {
  ClipboardCopy,
  CreditCard,
  MailCheck,
  MoreHorizontal,
} from "lucide-react";

export const studentsColumns: ColumnDef<Student>[] = [
  {
    accessorKey: "profilePicture",
    header: "Profile Picture",
    cell: ({ row }) => {
      const student = row.original;
      const [firstName, lastName] = student.fullName.split(" ");
      return (
        <Avatar className='m-auto'>
          <AvatarImage src={student.profilePicture} alt='' />
          <AvatarFallback className='text-blue-500 bg-blue-200'>
            {firstName?.[0]}
            {lastName?.[0]}
          </AvatarFallback>
        </Avatar>
      );
    },
    size: 90,
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
  },
  {
    id: "actions",
    enableHiding: false,
    size: 1,
    cell: ({ row }) => {
      const student = row.original;

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
            <DropdownMenuItem className='flex items-center gap-2 hover:cursor-pointer hover:bg-blue-600 hover:text-white'>
              <CreditCard />
              Add Credit
            </DropdownMenuItem>
            <DropdownMenuItem className='flex items-center gap-2 hover:cursor-pointer hover:bg-blue-600 hover:text-white'>
              <MailCheck /> Verify Email
            </DropdownMenuItem>
            <DropdownMenuItem className='flex items-center gap-2 hover:cursor-pointer hover:bg-blue-600 hover:text-white'>
              <MoreHorizontal />
              View Details
            </DropdownMenuItem>
          </DropdownMenuContent>
        </DropdownMenu>
      );
    },
  },
];
