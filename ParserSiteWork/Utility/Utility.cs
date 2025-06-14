namespace ParserSiteWork.Utility
{
    public class HtmlNamesFormatter
    {
        private string _module_name;
        private string _submodule_name;
        private string _sublist;

        public HtmlNamesFormatter(string module_name, string submodule_name, string sublist = "")
        {
            _module_name = module_name;
            _submodule_name = submodule_name;
            _sublist = sublist;
        }

        public string GetStartModelName(string submodule_name, string[] fields, int number)
        {
            return $"{_module_name}.{submodule_name}[{number}].{string.Join('.', fields)}";
        }

        public string GetCompetentionComponent(string[] fields, int number)
        {
            return GetStartModelName(_submodule_name, fields, number);
        }

        public string GetEvalComponent(string[] fields, int number, int eval_number)
        {
            string[] start_part = [$"{_sublist}[{eval_number}]"];

            return GetStartModelName(_submodule_name, start_part.Concat(fields).ToArray(), number);
        }
    }
}