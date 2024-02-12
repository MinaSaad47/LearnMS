import { Button } from "@/components/ui/button";
import {
  HoverCard,
  HoverCardContent,
  HoverCardTrigger,
} from "@/components/ui/hover-card";
import { AssistantIncome } from "@/types/assistants";
import { ColumnDef } from "@tanstack/react-table";

export const assistantIncomesColumns: ColumnDef<AssistantIncome>[] = [
  {
    accessorKey: "type",
    header: "Type",
    cell: ({ row }) => {
      const income = row.original;

      return (
        <HoverCard>
          <HoverCardTrigger asChild>
            <Button variant='link'>@{income.type}</Button>
          </HoverCardTrigger>
          <HoverCardContent className='font-bold border shadow-md text-primary w-fit border-secondary shadow-primary'>
            {income.type === "CodeSold" ? income.code : income.studentId}
          </HoverCardContent>
        </HoverCard>
      );
    },
  },
  {
    accessorKey: "amount",
    header: "Amount",
  },
  {
    accessorKey: "happenedAt",
    header: "Happened At",
    cell: ({ row }) => {
      const income = row.original;

      return <div>{new Date(income.happenedAt).toLocaleString()}</div>;
    },
  },
  {
    accessorKey: "claimedAt",
    header: "Claimed At",
    cell: ({ row }) => {
      const income = row.original;
      if (income.claimedAt) {
        return <div>{new Date(income.claimedAt).toLocaleString()}</div>;
      }
      return <div>Not Claimed</div>;
    },
  },
];
