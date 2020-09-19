DIR_GRAMMAR := grammar
DIR_GRAMMAR_GEN := src/fifthlang/Parser/gen
NAMESPACE := "fifth.Parser"
ANTLR := java -jar tools/antlr-4.8-complete.jar
ANTLR_FLAGS :=  -o $(DIR_GRAMMAR_GEN) \
								-package $(NAMESPACE) \
								-Dlanguage=CSharp \
								-visitor \
								-Werror

$(DIR_GRAMMAR_GEN)/grammar/fifthParser.cs $(DIR_GRAMMAR_GEN)/grammar/fifthLexer.cs: $(DIR_GRAMMAR)/fifth.g4
	$(ANTLR) $(ANTLR_FLAGS) $^

