import { Button } from "@/components/ui/button";
import { Checkbox } from "@/components/ui/checkbox";
import { CreditCode } from "@/types/credit-code";
import { ColumnDef } from "@tanstack/react-table";
import { ArrowUpDown, ShieldEllipsis } from "lucide-react";

export const creditCodesColumns: ColumnDef<CreditCode>[] = [
  {
    id: "select",
    header: ({ table }) => (
      <Checkbox
        checked={
          table.getIsAllPageRowsSelected() ||
          (table.getIsSomePageRowsSelected() && "indeterminate")
        }
        onCheckedChange={(value) => table.toggleAllPageRowsSelected(!!value)}
        aria-label='Select all'
      />
    ),
    cell: ({ row }) => (
      <Checkbox
        checked={row.getIsSelected()}
        onCheckedChange={(value) => row.toggleSelected(!!value)}
        aria-label='Select row'
      />
    ),
    enableSorting: false,
    enableHiding: false,
  },
  {
    accessorKey: "generator",
    header: "Generator",
    cell: ({ row }) => {
      const generator = row.original.generator;
      return (
        <div className='flex items-center justify-center'>
          {generator === null ? (
            <div className='flex items-center justify-center gap-1'>
              <ShieldEllipsis className='w-4 h-4 text-fuchsia-800' />
              by teacher
            </div>
          ) : (
            <div className='flex items-center justify-center gap-1'>
              <ShieldEllipsis className='w-4 h-4 text-gray-400' />
              {generator.email}
            </div>
          )}
        </div>
      );
    },
  },
  {
    accessorKey: "status",
    header: ({ column }) => {
      return (
        <Button
          variant='ghost'
          onClick={() => column.toggleSorting(column.getIsSorted() === "asc")}>
          Status
          <ArrowUpDown className='w-4 h-4 ml-2' />
        </Button>
      );
    },
  },
  {
    accessorKey: "code",
    header: "Code",
  },
  {
    accessorKey: "value",
    header: "Value",
  },
  {
    accessorKey: "redeemer",
    header: "Redeemer",
    cell: ({ row }) => {
      const redeemer = row.original.redeemer;
      return (
        <div className='flex items-center justify-center'>
          {redeemer === null ? (
            "not redeemed"
          ) : (
            <div className='flex items-center justify-center'>
              <ShieldEllipsis className='w-4 h-4 text-gray-400' />
              {redeemer.email}
            </div>
          )}
        </div>
      );
    },
  },
];
