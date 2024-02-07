import { useStudentsQuery } from "@/api/students-api";
import Loading from "@/components/loading/loading";
import { Button } from "@/components/ui/button";
import { studentsColumns } from "@/pages/dashboard/students/columns";
import { StudentsDataTable } from "@/pages/dashboard/students/data-table";
import { useModalStore } from "@/store/use-modal-store";
import { PaginationState } from "@tanstack/react-table";
import { Plus } from "lucide-react";
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
    <div
      className='flex flex-col w-full h-full p-4'
      onClick={(e) => openModal("add-student-modal")}>
      <Button className='self-end'>
        <Plus /> Add Student{" "}
      </Button>
      <div className='flex items-center justify-center'>
        {isLoading ? (
          <Loading />
        ) : (
          <StudentsDataTable
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
