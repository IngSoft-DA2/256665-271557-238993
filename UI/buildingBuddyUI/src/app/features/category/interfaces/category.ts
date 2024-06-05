export interface Category
{
    id: string,
    name : string,
    SubCategories? : Category[]
};
