using Rio.Data.RioObjectExtraction;

namespace Rio.Data.Extractions.CorrespondentDo
{
    public abstract class CorrespondentDoExtraction<TRioObject> : RioObjectExtraction<TRioObject, DataObjects.CorrespondentDo>
    {
        public override DataObjects.CorrespondentDo Extract(TRioObject rioObject)
        {
            return this.GetCorrespondent(rioObject);
        }

        protected virtual DataObjects.CorrespondentDo GetCorrespondent(TRioObject rioObject)
        {
            return null;
        }
    }
}
