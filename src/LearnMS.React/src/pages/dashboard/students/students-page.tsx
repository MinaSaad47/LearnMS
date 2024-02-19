import { useStudentsQuery } from "@/api/students-api";
import { DataTable } from "@/components/data-table";
import Loading from "@/components/loading/loading";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { studentsColumns } from "@/pages/dashboard/students/columns";
import { useModalStore } from "@/store/use-modal-store";
import { PaginationState } from "@tanstack/react-table";
import { Plus, Search } from "lucide-react";
import { useEffect, useState } from "react";
import { useSearchParams } from "react-router-dom";

const StudentsPage = () => {
  const { openModal } = useModalStore();
  const [searchParams, setSearchParams] = useSearchParams({});

  const [{ pageIndex, pageSize }, setPagination] = useState<PaginationState>({
    pageIndex: parseInt(searchParams.get("page") || "1") - 1,
    pageSize: parseInt(searchParams.get("pageSize") || "10"),
  });
  const [search, setSearch] = useState(searchParams.get("search") ?? "");
  const { data: students, isLoading } = useStudentsQuery({
    page: pageIndex + 1,
    pageSize,
    search,
  });

  useEffect(() => {
    setSearchParams({
      page: `${pageIndex + 1}`,
      pageSize: `${pageSize}`,
      ...(search ? { search } : {}),
    });
  }, [pageIndex, pageSize, search]);

  return (
    <div className='flex flex-col w-full h-full p-4'>
      <div className='flex items-center justify-between gap-2 p-4 border-blue-400 border-[2px] rounded-xl'>
        <Search className='text-blue-400' />
        <Input
          className='text-blue-400'
          onChange={(e) => setSearch(e.target.value)}
          value={search}
        />
        <Button
          onClick={() => openModal("add-student-modal")}
          variant='ghost'
          className='self-end text-blue-600 transition-all duration-300 bg-white hover:bg-blue-400 hover:text-white hover:scale-105'>
          <Plus /> Add Student{" "}
        </Button>
      </div>
      <div className='flex items-center justify-center'>
        {isLoading ? (
          <Loading />
        ) : (
          <DataTable
            pagination={{
              hasNextPage: students?.data!.hasNextPage!,
              hasPreviousPage: students?.data!.hasPreviousPage!,
              pageCount: students?.data!.totalCount!,
              pageIndex,
              pageSize,
            }}
            setPagination={setPagination}
            data={students?.data!.items!}
            columns={studentsColumns}
          />
        )}
      </div>
    </div>
  );
};

export default StudentsPage;
